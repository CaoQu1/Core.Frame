using Microsoft.AspNetCore.Builder;
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
        /// 宿主环境
        /// </summary>
        public static IHostingEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// 服务集合
        /// </summary>
        public static IServiceCollection ServiceCollection { get; set; }

        /// <summary>
        /// 请求信息
        /// </summary>
        public static IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        /// 主应用程序
        /// </summary>
        public static IApplicationBuilder ApplicationBuilder { get; set; }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>()
        {
            try
            {
                return HttpContextAccessor.HttpContext.RequestServices.GetService<T>();
            }
            catch 
            {
                return ApplicationBuilder.ApplicationServices.GetService<T>();
            }
        }

        /// <summary>
        /// 获取根目录
        /// </summary>
        /// <returns></returns>
        public static string CPWebRootPath()
        {
            if (HostingEnvironment.IsDevelopment())
            {
                return "";
            }
            else if (HostingEnvironment.IsProduction())
            {
                //return "/CPSite";
                return "";
            }
            else if (HostingEnvironment.IsStaging())
            {
                //return "/CPSite";
                return "";
            }
            else
                return "";
        }
    }
}
