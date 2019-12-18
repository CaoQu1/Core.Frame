using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Core.Global;
using Core.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Core.Domain;
using UEditorNetCore;

namespace Core.Frame.Web
{
    public class Startup
    {
        /// <summary>
        /// ע�������ļ�
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            WebHostEnvironment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// �����ļ�
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ��������
        /// </summary>
        public IWebHostEnvironment WebHostEnvironment { get; set; }

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
            services.Configure<CoreWebSite>(Configuration.GetSection("CoreWebSite"));
            services.Configure<CoreUpload>(Configuration.GetSection("CoreUpload"));

            var assmebies = new System.IO.DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).GetFiles("Core.Application*.dll")
                .Select(x => System.Reflection.Assembly.LoadFrom(x.FullName)).ToArray();
            services.AddMvc(option =>
            {
            }).AddApplicationPart(assmebies.FirstOrDefault())
                .AddRazorOptions(options =>
            {
                options.AreaViewLocationFormats.Add("/Core/Admin/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Core/Admin/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Core/Admin/Shared/{0}.cshtml");

                options.ViewLocationFormats.Add("/Core/Mobile/{1}/{0}.cshtml");
                options.ViewLocationFormats.Add("/Core/Mobile/{0}.cshtml");
                options.ViewLocationFormats.Add("/Core/Mobile/Shared/{0}.cshtml");
            }).AddJsonOptions(option =>
            {
                option.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });
            services.AddDbContext<BaseDbContext>(option =>
            {
                var connectionString = Configuration.GetConnectionString("Core");
                option.UseSqlServer(connectionString);
                var logFactory = new LoggerFactory();
                logFactory.AddLog4Net();
                option.UseLoggerFactory(logFactory);//���sql�����־
            });//��ʼ�����ݿ�����
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());//��Ӷ���ӳ�����
            services.AddHttpContextAccessor();
            services.AddAuthentication(option =>
            {
                option.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                option.RequireAuthenticatedSignIn = false;
            }).AddCookie(option =>
            {
                option.LoginPath = "/User/Login";
                option.LogoutPath = "/User/LoginOut";
                option.Events.OnSigningIn = (context) =>
                {
                    var cookieName = context.Options.Cookie.Name;
                    return Task.CompletedTask;
                };
            });
            services.AddSession();
            if (WebHostEnvironment.IsDevelopment())
            {
                services.AddMemoryCache();
            }
            else
            {
                services.AddDistributedRedisCache(option =>
                {
                    option.Configuration = "127.0.0.1";
                    option.InstanceName = "db0";
                });
            }
            services.AddCommonService()
                .AddRepositoryService()
                .AddDomainService();

            services.AddUEditorService();
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
            CoreAppContext.ApplicationBuilder = app;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCustomException(option =>
            {
                option.ErrorHandingPath = "/admin/home/error";
                option.HandleType = CustomExceptionHandleType.Both;
                option.JsonHandleUrlKeys = new PathString[] { "/api" };
            });

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
