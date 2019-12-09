using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public class CoreResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 结果
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 返回对象结果
    /// </summary>
    public class CoreResult:CoreResult<Object>
    {

    }
}
