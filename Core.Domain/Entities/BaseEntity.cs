using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseEntity<TEntity, TKey> : IEntity<TKey>, IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity<TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public TKey Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 初始化值
        /// </summary>
        public virtual void FormatInitValue() { }

        /// <summary>
        /// 获取hashcode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// 比较是否相等
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var value = obj as BaseEntity<TEntity, TKey>;
            if (value != null)
            {
                return value.Id.Equals(this.Id);
            }
            return Object.ReferenceEquals(obj, this);
        }

        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }

    /// <summary>
    /// 实体基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseUserEntity<TEntity, TUser, TKey> : BaseEntity<TEntity, TKey> where TEntity : class, IUserEntity<TKey, TUser>
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int UpdateUserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public virtual TUser CreateUser { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public virtual TUser UpdateUser { get; set; }

        /// <summary>
        /// 数据库配置
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
