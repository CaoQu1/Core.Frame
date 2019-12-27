using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 聚合根基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class AggregateRoot<TEntity, TKey> : BaseUserEntity<TEntity, TKey>, IAggregateRoot<TKey> where TEntity : class, IUserEntity<TKey>
    {
    }
}
