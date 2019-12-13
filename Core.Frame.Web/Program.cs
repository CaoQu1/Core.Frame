using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Core.Frame.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((host, log) =>
                {
                    log.AddFilter("System", LogLevel.Warning); //过滤掉系统默认的一些日志
                    log.AddFilter("Microsoft", LogLevel.Warning);//过滤掉系统默认的一些日志

                    log.AddLog4Net();//添加日志组件
                });
    }
}
