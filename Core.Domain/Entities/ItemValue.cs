using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 数据项值
    /// </summary>
    public class ItemValue : AggregateRoot<ItemValue, int>
    {
        /// <summary>
        /// 值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 数据项Id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// 数据项
        /// </summary>
        public virtual Item Item { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ItemValue> builder)
        {
            builder.ToTable("ItemValues");
            builder.HasOne(x => x.Item).WithMany(y => y.ItemValues).HasForeignKey(f => f.ItemId);
            base.Configure(builder);
        }
    }
}
