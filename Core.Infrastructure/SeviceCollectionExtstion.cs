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

            services.TryAddSingleton<IJsonSerializerService, JsonSerializerService>();//json序列化服务
            services.TryAddSingleton<ICacheManagerService, CacheManagerService>();//缓存服务
            services.TryAddSingleton<IEncryptionService, EncryptionService>();//加密服务
            services.TryAddSingleton<IVerifyCodeService, VerifyCodeService>();//验证码服务
            services.TryAddSingleton<IHttpRequestService, HttpRequestService>();//http请求服务

            services.TryAddTransient<IMenuRepository, MenuRepository>();
            services.TryAddTransient<ISystemUserRepository, SystemUserRepository>();
            services.TryAddTransient<SystemUserService, SystemUserService>();
            return services;
        }
    }
}
