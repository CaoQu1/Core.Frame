using AutoMapper;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Applications
{
    /// <summary>
    /// 应用接口基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseApplication<TKey, TEntity> : IBaseApplication<TEntity> where TEntity : class, IAggregateRoot<TKey>
    {
        protected readonly IDomainService<TKey, TEntity> _domainService;
        protected readonly IMapper _mapper;

        public BaseApplication(IDomainService<TKey, TEntity> domainService, IMapper mapper)
        {
            this._domainService = domainService;
            this._mapper = mapper;
        }

        public TEntity MapTo<TDto>(TDto dto)
        {
            return this._mapper.Map<TEntity>(dto);
        }

        public TDto MapForm<TDto>(TEntity entity)
        {
            return this._mapper.Map<TDto>(entity);
        }
    }
}
