using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    /// <summary>
    /// 聚合根接口
    /// </summary>
    public interface IAggregateRoot<TKey> : IEntity<TKey>
    {

    }
}
