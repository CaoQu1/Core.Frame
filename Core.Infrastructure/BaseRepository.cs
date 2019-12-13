using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Remotion.Linq.Parsing.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Core.Infrastructure
{
    /// <summary>
    /// 泛型仓储基类实现
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, IAggregateRoot<TKey>
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        private readonly IDbContext _dbContext;
        //private static readonly TypeInfo QueryCompilerTypeInfo = typeof(QueryCompiler).GetTypeInfo();
        //private static readonly FieldInfo QueryCompilerField = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields.First(x => x.Name == "_queryCompiler");
        //private static readonly PropertyInfo NodeTypeProviderField = QueryCompilerTypeInfo.DeclaredProperties.Single(x => x.Name == "NodeTypeProvider");
        //private static readonly MethodInfo CreateQueryParserMethod = QueryCompilerTypeInfo.DeclaredMethods.First(x => x.Name == "CreateQueryParser");
        //private static readonly FieldInfo DataBaseField = QueryCompilerTypeInfo.DeclaredFields.Single(x => x.Name == "_database");
        //private static readonly PropertyInfo DatabaseDependenciesProperty = typeof(Microsoft.EntityFrameworkCore.Storage.Database).GetTypeInfo().DeclaredProperties.Single(x => x.Name == "Dependencies");


        /// <summary>
        /// 日志
        /// </summary>
        private ILogger<BaseRepository<TKey, TEntity>> _logger;

        /// <summary>
        /// 数据集
        /// </summary>
        public IQueryable<TEntity> Table
        {
            get
            {
                return this._dbContext.Set<TEntity>();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BaseRepository(IDbContext dbContext, ILogger<BaseRepository<TKey, TEntity>> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        ///// <summary>
        ///// 用于监控生成的sql语句
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns></returns>
        //public string GetTraceString<T>(IQueryable<T> query)
        //{
        //    var str = query.ToString();
        //    var provider = query.Provider;
        //    var fields = typeof(EntityQueryProvider).GetTypeInfo().DeclaredFields;
        //    var properties = typeof(EntityQueryProvider).GetTypeInfo().DeclaredProperties;
        //    try
        //    {
        //        var queryCompiler = QueryCompilerField.GetValue(query.Provider);
        //        var nodeTypeProvider = (INodeTypeProvider)NodeTypeProviderField.GetValue(queryCompiler);
        //        var parser = (IQueryParser)CreateQueryParserMethod.Invoke(queryCompiler, new object[] { nodeTypeProvider });
        //        var queryModel = parser.GetParsedQuery(query.Expression);
        //        var database = DataBaseField.GetValue(queryCompiler);
        //        var databaseDependencies = (DatabaseDependencies)DatabaseDependenciesProperty.GetValue(database);
        //        var queryCompilationContextFactory = databaseDependencies.QueryCompilationContextFactory;
        //        var queryCompilationContext = queryCompilationContextFactory.Create(false);
        //        var modelVisitor = (RelationalQueryModelVisitor)queryCompilationContext.CreateQueryModelVisitor();
        //        modelVisitor.CreateQueryExecutor<T>(queryModel);
        //        var sql = modelVisitor.Queries.First().ToString();

        //        return sql;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        throw ex;
        //    }
        //}

        /// <summary>
        ///添加实体 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TKey Add(TEntity entity)
        {
            this._dbContext.Set<TEntity>().Add(entity);
            this._dbContext.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            return this._dbContext.SaveChanges() > 0 ? entity.Id : default(TKey);
        }

        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Add(IList<TEntity> entities)
        {
            entities.ToList().ForEach(entity =>
            {
                this._dbContext.Set<TEntity>().Add(entity);
                this._dbContext.Entry<TEntity>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            });
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <param name="navigationProperties"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Get(params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            DbSet<TEntity> dbSet = this._dbContext.Set<TEntity>();
            IList<TEntity> col = null;
            if (navigationProperties != null &&
               navigationProperties.Length > 0)
            {
                var eagerLoadingProperty = navigationProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbSet.Include(eagerLoadingPath);
                for (int i = 1; i < navigationProperties.Length; i++)
                {
                    eagerLoadingProperty = navigationProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                col = dbquery.ToList();
            }
            else
            {
                col = dbSet.ToList();
            }
            col.ToList().ForEach(t =>
            {
                t.FormatInitValue();
            });
            return col;
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
            DbSet<TEntity> dbSet = this._dbContext.Set<TEntity>();
            T t = dbSet.Where(specification.GetExpression()).Max(maxExpression);
            return t;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        public virtual TEntity Get(TKey id, params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            TEntity entity = null;
            if (navigationProperties != null &&
              navigationProperties.Length > 0)
            {
                DbSet<TEntity> dbSet = this._dbContext.Set<TEntity>();
                var eagerLoadingProperty = navigationProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbSet.Include(eagerLoadingPath);
                for (int i = 1; i < navigationProperties.Length; i++)
                {
                    eagerLoadingProperty = navigationProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                entity = dbquery.Where(c => c.Id.Equals(id)).First();
            }
            else
            {
                entity = this._dbContext.Set<TEntity>().Find(id);
            }
            if (entity != null)
            {
                entity.FormatInitValue();
            }
            return entity;
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        public virtual TEntity Get(TKey id, ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            TEntity entity = null;
            if (navigationProperties != null &&
              navigationProperties.Length > 0)
            {
                DbSet<TEntity> dbSet = this._dbContext.Set<TEntity>();
                var eagerLoadingProperty = navigationProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbSet.Include(eagerLoadingPath);
                for (int i = 1; i < navigationProperties.Length; i++)
                {
                    eagerLoadingProperty = navigationProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                entity = dbquery.Where(c => c.Id.Equals(id)).Where(specification.GetExpression()).First();
            }
            else
            {
                entity = this._dbContext.Set<TEntity>().Find(id);
            }
            if (entity != null)
            {
                entity.FormatInitValue();
            }
            return entity;
        }

        /// <summary>
        /// 获取实体集
        /// </summary>
        /// <param name="specification"></param>
        /// <param name="eagerLoadingProperties"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetByCondition(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] navigationProperties)
        {
            return this.GetByCondition(specification, null, CoreEnum.SortOrder.Asc, navigationProperties);
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
            DbSet<TEntity> dbSet = this._dbContext.Set<TEntity>();
            IQueryable<TEntity> queryable = null; //this._dbContext.Set<TEntity>().Where(specification.GetExpression());
            if (navigationProperties != null &&
              navigationProperties.Length > 0)
            {
                var eagerLoadingProperty = navigationProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbSet.Include(eagerLoadingPath);
                for (int i = 1; i < navigationProperties.Length; i++)
                {
                    eagerLoadingProperty = navigationProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
            {
                queryable = dbSet.Where(specification.GetExpression());
            }
            if (orderBy != null)
            {
                if (orderType == CoreEnum.SortOrder.Asc)
                {
                    queryable = queryable.SortBy(orderBy);
                }
                else
                    queryable = queryable.SortByDescending(orderBy);
            }
            List<TEntity> col = queryable.ToList();
            col.ToList().ForEach(t => { t.FormatInitValue(); });
            return col;
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
            DbSet<TEntity> dbSet = this._dbContext.Set<TEntity>();
            IQueryable<TEntity> queryable = null;// this._dbContext.Set<TEntity>().Where(specification.GetExpression());
            if (navigationProperties != null &&
             navigationProperties.Length > 0)
            {
                var eagerLoadingProperty = navigationProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                var dbquery = dbSet.Include(eagerLoadingPath);
                for (int i = 1; i < navigationProperties.Length; i++)
                {
                    eagerLoadingProperty = navigationProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    dbquery = dbquery.Include(eagerLoadingPath);
                }
                queryable = dbquery.Where(specification.GetExpression());
            }
            else
            {
                queryable = dbSet.Where(specification.GetExpression());
            }
            IQueryable<TEntity> queryableData = this._dbContext.Set<TEntity>().Where(specification.GetExpression());
            if (orderType == CoreEnum.SortOrder.Asc)
            {
                queryableData = queryable.SortBy(orderBy);
            }
            else
            {
                queryableData = queryable.SortByDescending(orderBy);
            }
            queryableData = queryableData.Skip(currentPage * (currentPage - 1))//跳过行数，最终生成的sql语句是Top(n)
                .Take(pageSize).AsQueryable();//返回指定数量的行 
            int nCount = queryable.Count();//获取总记录数

            List<TEntity> col = queryableData.ToList();
            return new CorePageResult<TEntity>(nCount,
                (nCount + pageSize - 1) / pageSize,
                pageSize,
                currentPage, col);
        }

        /// <summary>
        /// 获取实体的数量
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public int Count(ISpecification<TEntity> specification)
        {
            DbSet<TEntity> dbSet = this._dbContext.Set<TEntity>();
            return dbSet.Where(specification.GetExpression()).Count();
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IList<TEntity> SqlQuery(string sql)
        {
            List<TEntity> col = this._dbContext.Set<TEntity>().FromSqlRaw<TEntity>(sql).ToList();
            col.ForEach(t =>
            {
                t.FormatInitValue();
            });
            return col;
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            var dbEntity = this._dbContext.Entry<TEntity>(entity);
            if (dbEntity.State == EntityState.Modified)
            {
                var entityToUpdate = this._dbContext.Set<TEntity>().Find(entity.Id);
                return this._dbContext.SaveChanges();
            }
            return 0;
        }

        /// <summary>
        /// 更新实体集
        /// </summary>
        /// <param name="entityCol"></param>
        /// <returns></returns>
        public virtual int Update(IList<TEntity> entityCol)
        {
            entityCol.ToList().ForEach(t =>
            {
                var dbEntity = this._dbContext.Entry<TEntity>(t);
                if (dbEntity.State == EntityState.Modified)
                {
                    var entityToUpdate = this._dbContext.Set<TEntity>().Find(t.Id);
                }
            });
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(TKey id)
        {
            TEntity entity = this.Get(id);
            this._dbContext.Set<TEntity>().Remove(entity);
            this._dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            this._dbContext.Set<TEntity>().Remove(entity);
            this._dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除实体集
        /// </summary>
        /// <param name="entityCol"></param>
        /// <returns></returns>
        public virtual int Delete(IList<TEntity> entityCol)
        {
            entityCol.ToList().ForEach(t =>
            {
                this._dbContext.Set<TEntity>().Remove(t);
                this._dbContext.Entry<TEntity>(t).State = EntityState.Deleted;
            });
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除实体集
        /// </summary>
        /// <param name="idCol"></param>
        /// <returns></returns>
        public int Delete(List<TKey> idCol)
        {
            idCol.ForEach(t =>
            {
                TEntity entity = this.Get(t);
                this._dbContext.Set<TEntity>().Remove(entity);
                this._dbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            });
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        public virtual int DeleteByCondition(Expression<Func<TEntity, bool>> whereCondition)
        {
            Specification<TEntity> specification = ExpressionSpecification<TEntity>.Eval(whereCondition);
            IList<TEntity> col = this.GetByCondition(specification);
            this._dbContext.Set<TEntity>().RemoveRange(col);
            col.ToList().ForEach(t =>
            {
                this._dbContext.Entry<TEntity>(t).State = EntityState.Deleted;
            });
            return this._dbContext.SaveChanges();
        }

        /// <summary>
        /// 获取表达式主体
        /// </summary>
        /// <param name="lambda"></param>
        /// <returns></returns>
        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =
                    ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        /// <summary>
        ///获取导航属性名称
        /// </summary>
        /// <param name="eagerLoadingProperty"></param>
        /// <returns></returns>
        protected string GetEagerLoadingPath(Expression<Func<TEntity, dynamic>> eagerLoadingProperty)
        {
            MemberExpression memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");
            return path;
        }
    }
}
