using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Global
{

    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICacheManagerService
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        Task Add<T>(string key, T value, TimeSpan? timeSpan);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> Get<T>(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        Task Remove(string key);
    }

    /// <summary>
    /// 缓存实现
    /// </summary>
    public class CacheManagerService : CommonService<CacheManagerService>, ICacheManagerService
    {
        /// <summary>
        /// 分布式缓存接口
        /// </summary>
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// json解析接口
        /// </summary>
        private readonly IJsonSerializerService _jsonSerializerService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="distributedCache"></param>
        public CacheManagerService(IDistributedCache distributedCache, IJsonSerializerService jsonSerializerService)
        {
            this._distributedCache = distributedCache;
            this._jsonSerializerService = jsonSerializerService;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        public async Task Add<T>(string key, T value, TimeSpan? timeSpan) => await Invoke(async () =>
              {
                  string valueString = await this._jsonSerializerService.SerializeObject(value);
                  await this._distributedCache.SetAsync(key, Encoding.Default.GetBytes(valueString), new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = timeSpan });
              });

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> Get<T>(string key) => await Invoke<T>(async () =>
        {
            return await this._jsonSerializerService.DeserializeObject<T>(Encoding.Default.GetString(await this._distributedCache.GetAsync(key)));
        });

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        public async Task Remove(string key) => await Invoke(async () => await this._distributedCache.RemoveAsync(key));

    }

    /// <summary>
    /// 缓存扩展
    /// </summary>
    public static class CacheManagerExstion
    {
        /// <summary>
        /// 获取缓存，不存在增加缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cacheManagerService"></param>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task<T> GetOrAdd<T>(this ICacheManagerService cacheManagerService, string key, Func<T> action, TimeSpan? timeSpan)
        {
            var tInstance = await cacheManagerService.Get<T>(key);
            if (tInstance == null && action != null)
            {
                tInstance = action();
                await cacheManagerService.Add<T>(key, tInstance, timeSpan.HasValue ? timeSpan.Value : TimeSpan.FromMilliseconds(5));
            }
            return tInstance;
        }
    }
}
