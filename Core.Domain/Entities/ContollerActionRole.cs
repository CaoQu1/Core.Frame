using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 控制器角色对应关系实体
    /// </summary>
    public class ContollerActionRole : AggregateRoot<ContollerActionRole, int>
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 控制器编号
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 操作编号
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// 控制器实体
        /// </summary>
        public virtual ControllerPermissions ControllerPermissions { get; set; }

        /// <summary>
        /// 操作实体
        /// </summary>
        public virtual ActionPermissions ActionPermissions { get; set; }

        /// <summary>
        /// 配置数据库映射
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ContollerActionRole> builder)
        {
            builder.ToTable("ContollerActionRoles");
            builder.HasOne(x => x.ControllerPermissions).WithMany(y => y.ContollerActionRoles).HasForeignKey(f => f.ControllerId);
            builder.HasOne(x => x.ActionPermissions).WithMany(y => y.ContollerActionRoles).HasForeignKey(f => f.ActionId);
        }
    }
}
