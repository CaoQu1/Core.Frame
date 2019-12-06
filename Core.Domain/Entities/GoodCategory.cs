using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 商品类型实体
    /// </summary>
    [Table("GoodCategories")]
    public class GoodCategory : AggregateRoot<GoodCategory, int>
    {
        /// <summary>
        /// 类型名称
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// 类型描述
        /// </summary>
        public string CategoryDesc { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public virtual ICollection<Goods> Goods { get; set; }
    }
}
