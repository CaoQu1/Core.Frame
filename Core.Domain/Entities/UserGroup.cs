using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Global.CoreEnum;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 用户组别实体
    /// </summary>
    [Table("UserGroups")]
    public class UserGroup : AggregateRoot<UserGroup, int>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 获取红包方式
        /// </summary>
        public GetRedPackType? RedPackType { get; set; }

        /// <summary>
        /// 固定红包金额
        /// </summary>
        public decimal? FixedRedPacket { get; set; }

        /// <summary>
        /// 标识
        /// </summary>
        public UserType Identification { get; set; }
    }
}
