using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 数据项
    /// </summary>
    [Table("Items")]
    public class Item : AggregateRoot<Item, int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 数据值
        /// </summary>
        public virtual ICollection<ItemValue> ItemValues { get; set; }
    }
}
