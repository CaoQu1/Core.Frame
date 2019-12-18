using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Global
{

    /// <summary>
    /// http请求接口
    /// </summary>
    public interface IHttpRequestService
    {
        /// <summary>
        /// 异步请求(POST)
        /// </summary>
        /// <typeparam name="RqDto"></typeparam>
        /// <typeparam name="RpDto"></typeparam>
        /// <param name="rqDto"></param>
        /// <returns></returns>
        Task<RpDto> PostAsync<RqDto, RpDto>(RqDto rqDto, string serverUrl = "");

        /// <summary>
        /// 异步请求(GET)
        /// </summary>
        /// <typeparam name="RqDto"></typeparam>
        /// <typeparam name="RpDto"></typeparam>
        /// <param name="rqDto"></param>
        /// <returns></returns>
        Task<RpDto> GetAsync<RqDto, RpDto>(RqDto rqDto, string serverUrl = "");
    }

    /// <summary>
    ///  http请求服务
    /// </summary>
    public class HttpRequestService : CommonService<HttpRequestService>, IHttpRequestService
    {
        /// <summary>
        /// json序列化参数
        /// </summary>
        private readonly IJsonSerializerService _jsonSerializerService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="jsonSerializerService"></param>
        public HttpRequestService(IJsonSerializerService jsonSerializerService)
        {
            _jsonSerializerService = jsonSerializerService;
        }

        /// <summary>
        /// 异步请求(GET)
        /// </summary>
        /// <typeparam name="RqDto"></typeparam>
        /// <typeparam name="RpDto"></typeparam>
        /// <param name="rqDto"></param>
        /// <param name="serverUrl"></param>
        /// <returns></returns>
        public Task<RpDto> GetAsync<RqDto, RpDto>(RqDto rqDto, string serverUrl = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 异步请求(POST)
        /// </summary>
        /// <typeparam name="RqDto"></typeparam>
        /// <typeparam name="RpDto"></typeparam>
        /// <param name="rqDto"></param>
        /// <param name="serverUrl"></param>
        /// <returns></returns>
        public Task<RpDto> PostAsync<RqDto, RpDto>(RqDto rqDto, string serverUrl = "")
        {
            throw new NotImplementedException();
        }
    }
}
