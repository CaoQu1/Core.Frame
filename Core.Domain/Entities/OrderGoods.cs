using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 订单详情实体
    /// </summary>
    public class OrderGoods : AggregateRoot<OrderGoods, int>
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        public int GoodsId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 所需积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal GoodsPrice { get; set; }

        /// <summary>
        /// 真实价格
        /// </summary>
        public decimal RealPrice { get; set; }

        /// <summary>
        /// 订单信息
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<OrderGoods> builder)
        {
            builder.ToTable("OrderGoods");
            builder.HasOne(x => x.Order).WithMany(y => y.OrderGoods).HasForeignKey(f => f.OrderId);
            base.Configure(builder);
        }
    }
}
