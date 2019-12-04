using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ObjectExstion
    {
        /// <summary>
        /// 判断字符串是否为空或者为NULL
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value);
    }
}
