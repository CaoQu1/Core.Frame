using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace UEditorNetCore.Handlers
{
    /// <summary>
    /// ��������
    /// </summary>
    public abstract class Handler
    {
        /// <summary>
        /// ����
        /// </summary>
        public HttpRequest Request { get; private set; }

        /// <summary>
        /// ��Ӧ
        /// </summary>
        public HttpResponse Response { get; private set; }

        /// <summary>
        /// ����������
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
        /// ִ������
        /// </summary>
        public abstract void Process();

        /// <summary>
        /// �����Ӧ
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