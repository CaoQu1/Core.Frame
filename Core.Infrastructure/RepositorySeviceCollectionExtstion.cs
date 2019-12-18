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
    /// 服务扩展
    /// </summary>
    public static class RepositorySeviceCollectionExtstion
    {
        /// <summary>
        /// 注入数据仓储服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.TryAddTransient<IDbContext, BaseDbContext>();
            services.TryAddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));
            services.TryAddTransient<IMenuRepository, MenuRepository>();
            services.TryAddTransient<ISystemUserRepository, SystemUserRepository>();
            return services;
        }
    }
}
