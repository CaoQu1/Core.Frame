using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    public class CoreAppContext
    {
        /// <summary>
        /// 全局配置文件
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 服务集合
        /// </summary>
        public static IServiceCollection ServiceCollection { get; set; }

        /// <summary>
        /// 请求信息
        /// </summary>
        public static IHttpContextAccessor HttpContextAccessor { get; set; }
        
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            return HttpContextAccessor.HttpContext.RequestServices.GetService<T>();
        }
    }
}
