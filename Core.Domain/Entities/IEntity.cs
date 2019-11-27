using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 标识
        /// </summary>
        TKey Id { get; set; }

        /// <summary>
        /// 对值初始化
        /// </summary>
        void FormatInitValue();
    }
}
