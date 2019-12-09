using Core.Global;
using log4net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Web
{
    /// <summary>
    /// 异常中间件
    /// </summary>
    public class CustomExceptionMiddleWare
    {
        /// <summary>
        /// 管道请求委托
        /// </summary>
        private RequestDelegate _next;

        /// <summary>
        /// 配置对象
        /// </summary>
        private CustomExceptionMiddleWareOption _option;

        /// <summary>
        /// 需要处理的状态码字典
        /// </summary>
        private IDictionary<int, string> exceptionStatusCodeDic;

        /// <summary>
        /// json解析接口
        /// </summary>
        private readonly IJsonSerializerService _jsonSerializerService;

        /// <summary>
        /// 日志接口
        /// </summary>
        private readonly ILogger<CustomExceptionMiddleWare> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="option"></param>
        public CustomExceptionMiddleWare(RequestDelegate next,
            CustomExceptionMiddleWareOption option,
            IJsonSerializerService jsonSerializerService,
            ILogger<CustomExceptionMiddleWare> logger)
        {
            _next = next;
            _option = option;
            exceptionStatusCodeDic = new Dictionary<int, string>
            {
                { 401, "未授权的请求" },
                { 404, "找不到该页面" },
                { 403, "访问被拒绝" },
                { 500, "服务器发生意外的错误" }
                //其余状态自行扩展
            };
            _jsonSerializerService = jsonSerializerService;
            _logger = logger;
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            Exception exception = null;
            try
            {
                await _next(context);   //调用管道执行下一个中间件
            }
            catch (Exception ex)
            {
                context.Response.Clear();
                context.Response.StatusCode = 500;   //发生未捕获的异常，手动设置状态码
                exception = ex;
            }
            finally
            {
                if (exceptionStatusCodeDic.ContainsKey(context.Response.StatusCode) &&
                    !context.Items.ContainsKey("ExceptionHandled"))  //预处理标记
                {
                    var errorMsg = string.Empty;
                    if (context.Response.StatusCode == 500 && exception != null)
                    {
                        errorMsg = $"{exceptionStatusCodeDic[context.Response.StatusCode]}\r\n{(exception.InnerException != null ? exception.InnerException.Message : exception.Message)}";
                    }
                    else
                    {
                        errorMsg = exceptionStatusCodeDic[context.Response.StatusCode];
                    }
                    exception = new Exception(errorMsg);
                }
                if (exception != null)
                {
                    var handleType = _option.HandleType;
                    if (handleType == CustomExceptionHandleType.Both)   //根据Url关键字决定异常处理方式
                    {
                        var requestPath = context.Request.Path;
                        handleType = _option.JsonHandleUrlKeys != null && _option.JsonHandleUrlKeys.Count(
                            k => requestPath.StartsWithSegments(k, StringComparison.CurrentCultureIgnoreCase)) > 0 ?
                            CustomExceptionHandleType.JsonHandle :
                            CustomExceptionHandleType.PageHandle;
                    }

                    if (handleType == CustomExceptionHandleType.JsonHandle)
                        await JsonHandle(context, exception);
                    else
                        await PageHandle(context, exception, _option.ErrorHandingPath);
                    _logger.LogError(exception, exception.Message);
                }
            }
        }

        /// <summary>
        /// 统一格式响应类
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private CoreResult GetApiResponse(Exception ex)
        {
            return new CoreResult() { Success = false, Message = ex.Message };
        }

        /// <summary>
        /// 处理方式：返回Json格式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task JsonHandle(HttpContext context, Exception ex)
        {
            var apiResponse = GetApiResponse(ex);
            var serialzeStr = await _jsonSerializerService.SerializeObject(apiResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(serialzeStr, Encoding.UTF8);
        }

        /// <summary>
        /// 处理方式：跳转网页
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task PageHandle(HttpContext context, Exception ex, PathString path)
        {
            context.Items.Add("Exception", ex);
            var originPath = context.Request.Path;
            context.Request.Path = path;   //设置请求页面为错误跳转页面
            try
            {
                await _next(context);
            }
            catch { }
            finally
            {
                context.Request.Path = originPath;   //恢复原始请求页面
            }
        }
    }

    /// <summary>
    /// 异常中间件配置对象
    /// </summary>
    public class CustomExceptionMiddleWareOption
    {
        public CustomExceptionMiddleWareOption(
            CustomExceptionHandleType handleType = CustomExceptionHandleType.JsonHandle,
            IList<PathString> jsonHandleUrlKeys = null,
            string errorHandingPath = "")
        {
            HandleType = handleType;
            JsonHandleUrlKeys = jsonHandleUrlKeys;
            ErrorHandingPath = errorHandingPath;
        }

        /// <summary>
        /// 异常处理方式
        /// </summary>
        public CustomExceptionHandleType HandleType { get; set; }

        /// <summary>
        /// Json处理方式的Url关键字
        /// <para>仅HandleType=Both时生效</para>
        /// </summary>
        public IList<PathString> JsonHandleUrlKeys { get; set; }

        /// <summary>
        /// 错误跳转页面
        /// </summary>
        public PathString ErrorHandingPath { get; set; }
    }

    /// <summary>
    /// 错误处理方式
    /// </summary>
    public enum CustomExceptionHandleType
    {
        JsonHandle = 0,   //Json形式处理
        PageHandle = 1,   //跳转网页处理
        Both = 2          //根据Url关键字自动处理
    }

    /// <summary>
    /// 中间件扩展
    /// </summary>
    public static class CustomExceptionMiddleWareExtensions
    {
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder app, Action<CustomExceptionMiddleWareOption> action)
        {
            CustomExceptionMiddleWareOption customExceptionMiddleWareOption = new CustomExceptionMiddleWareOption();
            action(customExceptionMiddleWareOption);
            return app.UseMiddleware<CustomExceptionMiddleWare>(customExceptionMiddleWareOption);
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomExceptionMiddleWare>();
        }
    }
}
