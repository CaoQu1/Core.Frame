using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Global
{
    /// <summary>
    /// Json解析服务接口
    /// </summary>
    public interface IJsonSerializerService
    {
        /// <summary>
        /// 对象转Json字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<string> SerializeObject(object value);

        /// <summary>
        /// json字符串转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueString"></param>
        /// <returns></returns>
        Task<T> DeserializeObject<T>(string valueString);
    }

    /// <summary>
    /// Json解析服务
    /// </summary>
    public class JsonSerializerService : CommonService<JsonSerializerService>, IJsonSerializerService
    {
        /// <summary>
        /// cotr
        /// </summary>
        public JsonSerializerService()
        {
        }

        /// <summary>
        /// 对象转Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueString"></param>
        /// <returns></returns>
        public async Task<T> DeserializeObject<T>(string valueString) => await Invoke<T>(() => Task.Run(() => JsonConvert.DeserializeObject<T>(valueString)));

        /// <summary>
        /// json字符串转对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<string> SerializeObject(object value) => await Invoke<string>(() => Task.Run(() => JsonConvert.SerializeObject(value)));


    }
}
