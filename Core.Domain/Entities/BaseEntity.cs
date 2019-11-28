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
    public abstract class BaseEntity<TEntity, TKey> : IEntity<TKey>, IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        public TKey Id { get; set; }

        public virtual void FormatInitValue() { }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var value = obj as BaseEntity<TKey>;
            if (value != null)
            {
                return value.Id.Equals(this.Id);
            }
            return Object.ReferenceEquals(obj, this);
        }

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
