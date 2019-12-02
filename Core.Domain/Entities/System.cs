using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 系统实体
    /// </summary>
    [Table("Systems")]
    public class System : AggregateRoot<System, int>
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 主页路径
        /// </summary>
        public string HomeUrl { get; set; }
    }
}
