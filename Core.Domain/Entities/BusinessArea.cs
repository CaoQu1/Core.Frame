using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 业务区域实体
    /// </summary>
    [Table("BusinessAreas")]
    public class BusinessArea : AggregateRoot<BusinessArea, int>
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 父区域名称
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 用户实体
        /// </summary>
        public virtual User User { get; set; }
    }
}
