using Core.Global;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Global.CoreEnum;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 角色实体
    /// </summary>
    [Table("Roles")]
    public class Role : AggregateRoot<Role, int>
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDesc { get; set; }

        ///// <summary>
        ///// 获取红包方式
        ///// </summary>
        //public GetRedPackType? RedPackType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 角色用户关联信息
        /// </summary>
        public virtual ICollection<SystemUserRole> SystemUserRoles { get; set; }

        /// <summary>
        /// 角色权限关联信息
        /// </summary>
        public virtual ICollection<ContollerActionRole> ContollerActionRoles { get; set; }
    }
}
