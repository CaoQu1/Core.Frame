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

namespace Core.Frame.Web
{
    public class Startup
    {
        /// <summary>
        /// ע�������ļ�
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ����
        /// </summary>
        public IServiceCollection ServiceCollection { get; set; }

        /// <summary>
        /// ע�����
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assmebies = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles("Core.Application*.dll")
                .Select(x => System.Reflection.Assembly.LoadFrom(x.FullName)).ToArray();
            services.AddMvc()
                .AddApplicationPart(assmebies.FirstOrDefault())
                .AddRazorOptions(options =>
            {
                options.ViewLocationFormats.Add("/Core/{2}/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Core/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Core/Shared/{0}.cshtml");
            });
            services.AddDbContext<BaseDbContext>(option =>
            {
                var connectionString = Configuration.GetConnectionString("Core");
                option.UseSqlServer(connectionString);
                option.UseLoggerFactory(new LoggerFactory(new List<ILoggerProvider> { new Log4NetProvider() }));//���sql�����־
            });//��ʼ�����ݿ�����
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//��Ӷ���ӳ�����
            services.AddDomianService();
            ServiceCollection = services;
        }

        /// <summary>
        /// ����м��
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            CoreAppContext.HttpContextAccessor = app.ApplicationServices.GetService<IHttpContextAccessor>();
            CoreAppContext.ServiceCollection = ServiceCollection;
            CoreAppContext.Configuration = Configuration;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "/{Controller}/{Action}/{id?}", defaults: new
                {
                    Controller = "User",
                    Action = "Login"
                });
            });
        }
    }
}
