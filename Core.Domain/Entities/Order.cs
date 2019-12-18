using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Global.CoreEnum;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 订单实体
    /// </summary>
    [Table("Orders")]
    public class Order : AggregateRoot<Order, int>
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int? PaymentId { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        public int? DistributionId { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// 支付状态
        /// </summary>
        public PaymentStatus PaymentStatus { get; set; }

        /// <summary>
        /// 发货状态
        /// </summary>
        public DistributionStatus DistributionStatus { get; set; }

        /// <summary>
        /// 快递名称
        /// </summary>
        public string DeliveryName { get; set; }

        /// <summary>
        /// 快递单号
        /// </summary>
        public string DeliveryNo { get; set; }

        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string AcceptName { get; set; }

        /// <summary>
        /// 邮件编码
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 订单留言
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 收获地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 应付商品总金额
        /// </summary>
        public decimal? PayableAmount { get; set; }

        /// <summary>
        /// 实付商品总金额
        /// </summary>
        public decimal? RealAmount { get; set; }

        /// <summary>
        /// 应付运费
        /// </summary>
        public decimal? PayableFreight { get; set; }

        /// <summary>
        /// 实付运费
        /// </summary>
        public decimal? RealFreight { get; set; }

        /// <summary>
        /// 支付手续费
        /// </summary>
        public decimal? PaymentFee { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal? OrderAmount { get; set; }

        /// <summary>
        /// 所需积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? PaymentTime { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? ConfirmTime { get; set; }

        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? DistributionTime { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }

        /// <summary>
        /// 订单详情信息
        /// </summary>
        public virtual ICollection<OrderGoods> OrderGoods { get; set; }
    }
}
