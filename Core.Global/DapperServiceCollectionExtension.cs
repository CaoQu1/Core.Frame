using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static class DapperServiceCollectionExtension
    {
        /// <summary>
        /// 添加dapper
        /// </summary>
        /// <param name="services"></param>
        /// <param name="name"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IServiceCollection AddDapper(this IServiceCollection services, string name, Action<CoreConnection> action)
        {
            services.Configure<CoreConnection>(name, action);
            services.AddSingleton<IDapperFactory, DefaultDapperFactory>();
            return services;
        }
    }
}
