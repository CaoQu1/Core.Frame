using Core.Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static class DomainServiceCollectionExtension
    {
        /// <summary>
        /// 注入领域服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            services.TryAddTransient(typeof(BaseDomainService<,>), typeof(BaseDomainService<,>));
            services.TryAddTransient<SystemUserService, SystemUserService>();
            return services;
        }
    }
}
