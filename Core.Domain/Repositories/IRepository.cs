﻿using Core.Domain.Entities;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.Domain.Repositories
{
    /// <summary>
    /// 泛型仓储接口
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TKey, TEntity> where TEntity : class, IAggregateRoot<TKey>
    {
        TKey Add(TEntity entity);

        int Add(IList<TEntity> entities);

        TEntity Get(TKey id, params Expression<Func<TEntity, dynamic>>[] navigationProperties);

        TEntity Get(TKey id, ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] navigationProperties);

        IList<TEntity> Get(params Expression<Func<TEntity, dynamic>>[] navigationProperties);

        T GetMax<T>(Expression<Func<TEntity, T>> maxExpression, ISpecification<TEntity> specification);

        IList<TEntity> GetByCondition(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] navigationProperties);

        IList<TEntity> GetByCondition(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> orderBy, CoreEnum.SortOrder orderType, params Expression<Func<TEntity, dynamic>>[] navigationProperties);

        CorePageResult<List<TEntity>> GetByCondition(ISpecification<TEntity> specification,
       Expression<Func<TEntity, dynamic>> orderBy,
       CoreEnum.SortOrder orderType,
       int pageSize, int currentPage, params Expression<Func<TEntity, dynamic>>[] navigationProperties);

        IList<TEntity> SqlQuery(string sql);

        int Count(ISpecification<TEntity> specification);

        int Update(TEntity entity);

        int Update(IList<TEntity> entityCol);

        int Delete(TEntity entity);

        int Delete(IList<TEntity> entityCol);

        int Delete(TKey id);

        int Delete(List<TKey> idCol);

        int DeleteByCondition(Expression<Func<TEntity, bool>> whereCondition);

        IQueryable<TEntity> Table { get; }
    }
}
