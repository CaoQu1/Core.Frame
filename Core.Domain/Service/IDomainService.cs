using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Service
{
    /// <summary>
    /// 领域服务接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDomainService<TKey, TEntity> : IDomainService where TEntity : class, IAggregateRoot<TKey>
    {

    }

    /// <summary>
    /// 领域服务接口
    /// </summary>
    public interface IDomainService
    {

    }

    /// <summary>
    /// 值对象服务接口
    /// </summary>
    public interface IBaseService
    {

    }
}
