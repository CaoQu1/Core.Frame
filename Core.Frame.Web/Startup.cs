using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Core.Global;
using Core.Domain.Service;
using Core.Application.Filter;
using Core.Web;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Core.Frame.Web
{
    public class Startup
    {
        /// <summary>
        /// 注入配置文件
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置文件
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 服务
        /// </summary>
        public IServiceCollection ServiceCollection { get; set; }

        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assmebies = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles("Core.Application*.dll")
                .Select(x => System.Reflection.Assembly.LoadFrom(x.FullName)).ToArray();
            services.AddMvc(option =>
            {
                // option.Filters.Add(new CoreAuthorizationFilter());
            }).AddApplicationPart(assmebies.FirstOrDefault())
                .AddRazorOptions(options =>
            {
                options.AreaViewLocationFormats.Add("/Core/Admin/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Core/Admin/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Core/Admin/Shared/{0}.cshtml");

                options.ViewLocationFormats.Add("/Core/Mobile/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Core/Mobile/{0}.cshtml");
                options.ViewLocationFormats.Add("/Core/Mobile/Shared/{0}.cshtml");
            });
            services.AddDbContext<BaseDbContext>(option =>
            {
                var connectionString = Configuration.GetConnectionString("Core");
                option.UseSqlServer(connectionString);
                var logFactory = new LoggerFactory();
                logFactory.AddLog4Net("log4.config");
                option.UseLoggerFactory(logFactory);//添加sql监控日志
            });//初始化数据库连接
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//添加对象映射组件
            services.AddDomianService();
            services.AddHttpContextAccessor();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/User/Login";
                option.LogoutPath = "/User/LoginOut";
            });
            services.AddSession();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "127.0.0.1";
                option.InstanceName = "db0";
            });
            //ServiceCollection.AddOptions<CustomExceptionMiddleWareOption>();
            ServiceCollection.Configure<CoreWebSite>(Configuration.GetSection("CoreWebSite"));
            ServiceCollection = services;
        }

        /// <summary>
        /// 添加中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CoreAppContext.HttpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
            CoreAppContext.ServiceCollection = ServiceCollection;
            CoreAppContext.Configuration = Configuration;
            CoreAppContext.ApplicationBuilder = app;



            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            app.UseCustomException(option =>
            {
                option.ErrorHandingPath = "/home/error";
                option.HandleType = CustomExceptionHandleType.Both;
                option.JsonHandleUrlKeys = new PathString[] { "/api" };
            });
            //app.UseHsts();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseWap();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(name: "admin", areaName: "Admin", pattern: "/Admin/{Controller=Admin}/{Action=Login}/{id?}");
                endpoints.MapControllerRoute(name: "default", pattern: "/{Controller}/{Action}/{id?}", defaults: new
                {
                    Controller = "User",
                    Action = "Login"
                });
            });
        }
    }
}
