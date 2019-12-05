using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web
{
    /// <summary>
    /// 判断请求来源
    /// </summary>
    public class WapMiddware
    {
        /// <summary>
        /// 后续请求
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public WapMiddware(RequestDelegate next)
        {
            this._next = next;
        }

        /// <summary>
        /// 执行判断
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext httpContext)
        {
            var userAgents = httpContext.Request.Headers["user-agent"];
            if (!httpContext.Request.Path.HasValue || httpContext.Request.Path.Value == "/")
            {
                if (userAgents.Contains("micromessenger"))
                {
                    httpContext.Request.Path = new PathString("/mobile/user/login");
                }
                else
                {
                    httpContext.Request.Path = new PathString("/admin/admin/login");
                }
            }
            await _next(httpContext);
        }
    }

    /// <summary>
    /// 扩展
    /// </summary>
    public static class WapMiddwareExstion
    {
        /// <summary>
        /// 判断中间件
        /// </summary>
        /// <param name="applicationBuilder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseWap(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<WapMiddware>();
            return applicationBuilder;
        }
    }
}
