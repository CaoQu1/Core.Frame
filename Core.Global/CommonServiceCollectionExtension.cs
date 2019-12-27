using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static class CommonServiceCollectionExtension
    {
        /// <summary>
        /// 注入公共服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCommonService(this IServiceCollection services)
        {
            services.TryAddSingleton<IJsonSerializerService, JsonSerializerService>();//json序列化服务
            services.TryAddSingleton<ICacheManagerService, CacheManagerService>();//缓存服务
            services.TryAddSingleton<IEncryptionService, EncryptionService>();//加密服务
            services.TryAddSingleton<IVerifyCodeService, VerifyCodeService>();//验证码服务
            services.TryAddSingleton<IHttpRequestService, HttpRequestService>();//http请求服务
            return services;
        }
    }
}
