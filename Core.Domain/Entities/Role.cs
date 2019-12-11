using Core.Global;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role
            {
                RoleName = "系统管理员",
                RoleDesc = "超级权限",
                Status = Global.CoreEnum.Status.Enable,
                CreateTime = DateTime.Now,
                SortId = 1,
                SystemId = 1,
                Id = 1
            });
            base.Configure(builder);
        }
    }
}
