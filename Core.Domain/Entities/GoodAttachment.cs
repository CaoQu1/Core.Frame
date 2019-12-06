using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 商品附件实体
    /// </summary>
    public class GoodAttachment : AggregateRoot<GoodAttachment, int>
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int GoodId { get; set; }

        /// <summary>
        /// 附件编号
        /// </summary>
        public int AttachmentId { get; set; }

        /// <summary>
        /// 是否是幻灯片
        /// </summary>
        public bool? IsSlide { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public virtual Goods Goods { get; set; }

        /// <summary>
        /// 附件信息
        /// </summary>
        public virtual SystemAttachment SystemAttachment { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<GoodAttachment> builder)
        {
            builder.ToTable("GoodAttachments");
            builder.HasOne(x => x.Goods).WithMany(y => y.GoodAttachments).HasForeignKey(f => f.GoodId);
            builder.HasOne(x => x.SystemAttachment).WithMany(y => y.GoodAttachments).HasForeignKey(f => f.AttachmentId);
            base.Configure(builder);
        }
    }
}
