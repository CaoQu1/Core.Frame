using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Infrastructure
{
    /// <summary>
    /// 注入扩展
    /// </summary>
    public static class SeviceCollectionExtstion
    {
        /// <summary>
        /// 注入领域服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddDomianService(this IServiceCollection services)
        {
            services.TryAddTransient<IDbContext, BaseDbContext>();
            services.TryAddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.TryAddTransient(typeof(BaseDomainService<,>), typeof(BaseDomainService<,>));
            services.TryAddSingleton<IJsonSerializerService, JsonSerializerService>();
            services.TryAddTransient<ICacheManagerService, CacheManagerService>();
            services.TryAddTransient<IEncryptionService, EncryptionService>();
            services.TryAddTransient<IVerifyCodeService, VerifyCodeService>();
            services.TryAddTransient<SystemUserService, SystemUserService>();
            return services;
        }
    }
}
