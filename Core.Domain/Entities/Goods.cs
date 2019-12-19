using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 商品实体
    /// </summary>
    public class Goods : AggregateRoot<Goods, int>
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string GoodNo { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string GoodName { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// 所需积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 是否幻灯片
        /// </summary>
        public bool? IsSlide { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public int GoodCategoryId { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailImg { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        public string DetailContent { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public virtual GoodCategory GoodCategory { get; set; }

        /// <summary>
        /// 商品附件（幻灯片）
        /// </summary>
        public virtual ICollection<GoodAttachment> GoodAttachments { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Goods> builder)
        {
            builder.ToTable("Goods");
            builder.HasOne(x => x.GoodCategory).WithMany(y => y.Goods).HasForeignKey(f => f.GoodCategoryId);
            base.Configure(builder);
        }
    }
}
