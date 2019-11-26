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
        TKey Id { get; set; }
    }
}
