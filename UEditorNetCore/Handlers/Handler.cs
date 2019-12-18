using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace UEditorNetCore.Handlers
{
    /// <summary>
    /// 抽象处理类
    /// </summary>
    public abstract class Handler
    {
        /// <summary>
        /// 请求
        /// </summary>
        public HttpRequest Request { get; private set; }

        /// <summary>
        /// 响应
        /// </summary>
        public HttpResponse Response { get; private set; }

        /// <summary>
        /// 请求上下文
        /// </summary>
        public HttpContext Context { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public Handler(HttpContext context)
        {
            this.Request = context.Request;
            this.Response = context.Response;
            this.Context = context;
        }

        /// <summary>
        /// 执行请求
        /// </summary>
        public abstract void Process();

        /// <summary>
        /// 输出响应
        /// </summary>
        /// <param name="response"></param>
        protected void WriteJson(object response)
        {
            string jsonpCallback = Context.Request.Query["callback"],
                json = JsonConvert.SerializeObject(response);
            if (String.IsNullOrWhiteSpace(jsonpCallback))
            {
                Response.Headers.Add("Content-Type", "text/plain");
                Response.WriteAsync(json);
            }
            else
            {
                Response.Headers.Add("Content-Type", "application/javascript");
                Response.WriteAsync(String.Format("{0}({1});", jsonpCallback, json));
            }
        }
    }
}