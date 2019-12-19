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
    public class ControllerRole : AggregateRoot<ControllerRole, int>
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// 控制器操作编号
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// 控制器实体
        /// </summary>
        public virtual ControllerPermissions ControllerPermissions { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 配置数据库映射
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ControllerRole> builder)
        {
            builder.ToTable("ContollerRoles");
            builder.HasOne(x => x.ControllerPermissions).WithMany(y => y.ContollerRoles).HasForeignKey(f => f.ControllerId);
            builder.HasOne(x => x.Role).WithMany(y => y.ContollerRoles).HasForeignKey(f => f.RoleId);

            builder.HasData(new ControllerRole
            {
                RoleId = 1,
                SortId = 1,
                SystemId = 1,
                CreateTime = DateTime.Now,
                Id = 1,
                ControllerId = 1,
            });

            builder.HasData(new ControllerRole
            {
                RoleId = 1,
                SortId = 2,
                SystemId = 1,
                CreateTime = DateTime.Now,
                Id = 2,
                ControllerId = 2,
            });

            base.Configure(builder);
        }
    }
}
