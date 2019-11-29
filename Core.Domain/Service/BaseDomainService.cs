using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.Domain.Service
{
    /// <summary>
    /// 领域服务基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class BaseDomainService<TKey, TEntity> : IDomainService<TKey, TEntity> where TEntity : class, IAggregateRoot<TKey>
    {
        protected readonly IRepository<TKey, TEntity> _repository;
        protected readonly ILogger<BaseDomainService<TKey, TEntity>> _logger;

        public BaseDomainService(IRepository<TKey, TEntity> repository, ILogger<BaseDomainService<TKey, TEntity>> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        /// <summary>
        ///添加实体 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Add(TEntity entity)
        {
            return Invoke<int>(() => this._repository.Add(entity), entity);
        }

        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Add(IList<TEntity> entities)
        {
            return this._repository.Add(entities);
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Get(params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            return this._repository.Get(navigationProperties);
        }

        /// <summary>
        /// 获取最大值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="maxExpression"></param>
        /// <param name="specification"></param>
        /// <returns></returns>
        public virtual T GetMax<T>(Expression<Func<TEntity, T>> maxExpression, ISpecification<TEntity> specification)
        {
            return this._repository.GetMax(maxExpression, specification);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        public virtual TEntity Get(TKey id, params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            return this._repository.Get(id, navigationProperties);
        }

        /// <summary>
        /// 获取实体集
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetByCondition(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            return this._repository.GetByCondition(specification, navigationProperties);
        }

        /// <summary>
        /// 获取实体集
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetByCondition(ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> orderBy,
          CoreEnum.SortOrder orderType
            , params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            return this._repository.GetByCondition(specification, orderBy, orderType, navigationProperties);
        }

        /// <summary>
        /// 获取分页实体集
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderType"></param>
        /// <param name="pageSize"></param>
        /// <param name="currentPage"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        public virtual CorePageResult<TEntity> GetByCondition(
            ISpecification<TEntity> specification,
            Expression<Func<TEntity, dynamic>> orderBy,
               CoreEnum.SortOrder orderType,
            int pageSize, int currentPage
            , params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            return this._repository.GetByCondition(specification, orderBy, orderType, pageSize, currentPage, navigationProperties);
        }

        /// <summary>
        /// 获取实体的数量
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public int Count(ISpecification<TEntity> specification)
        {
            return this._repository.Count(specification);
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IList<TEntity> SqlQuery(string sql)
        {
            return this._repository.SqlQuery(sql);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            return this._repository.Update(entity);
        }

        /// <summary>
        /// 更新实体集
        /// </summary>
        /// <param name="entityCol"></param>
        /// <returns></returns>
        public virtual int Update(IList<TEntity> entityCol)
        {
            return this._repository.Update(entityCol);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(TKey id)
        {
            return this._repository.Delete(id);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            return this._repository.Delete(entity);
        }

        /// <summary>
        /// 删除实体集
        /// </summary>
        /// <param name="entityCol"></param>
        /// <returns></returns>
        public virtual int Delete(IList<TEntity> entityCol)
        {
            return this._repository.Delete(entityCol);
        }

        /// <summary>
        /// 删除实体集
        /// </summary>
        /// <param name="idCol"></param>
        /// <returns></returns>
        public int Delete(List<TKey> idCol)
        {
            return this._repository.Delete(idCol);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public virtual int DeleteByCondition(Expression<Func<TEntity, bool>> whereCondition)
        {
            return this._repository.DeleteByCondition(whereCondition);
        }

        /// <summary>
        /// 执行前检查
        /// </summary>
        protected Func<TEntity, bool> CheckEntity { get; set; }

        /// <summary>
        /// 捕获异常日志
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        public ReturnT Invoke<ReturnT>(Func<ReturnT> action, TEntity entity = null)
        {
            try
            {
                bool flag = true;
                if (CheckEntity != null && entity != null)
                {
                    flag = CheckEntity(entity);
                }
                if (flag)
                {
                    if (action != null)
                    {
                        return (ReturnT)action.Invoke();
                    }
                }
                else
                {
                    _logger.LogError($"{nameof(TEntity)}实体检查失败!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return default(ReturnT);
        }
    }
}
