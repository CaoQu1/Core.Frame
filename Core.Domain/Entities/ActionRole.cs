using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class ActionRole : AggregateRoot<ActionRole, int>
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
        public int ActionId { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// 控制器实体
        /// </summary>
        public virtual ActionPermissions ActionPermissions { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 配置数据库映射
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ActionRole> builder)
        {
            builder.ToTable("ActionRoles");
            builder.HasOne(x => x.ActionPermissions).WithMany(y => y.ActionRoles).HasForeignKey(f => f.ActionId);
            builder.HasOne(x => x.Role).WithMany(y => y.ActionRoles).HasForeignKey(f => f.RoleId);

            builder.HasData(new ActionRole
            {
                RoleId = 1,
                SortId = 1,
                SystemId = 1,
                CreateTime = DateTime.Now,
                Id = 1,
                ActionId = 1,
                ControllerId = 1
            });

            builder.HasData(new ActionRole
            {
                RoleId = 1,
                SortId = 2,
                SystemId = 1,
                CreateTime = DateTime.Now,
                Id = 2,
                ActionId = 2,
                ControllerId = 2
            });

            base.Configure(builder);
        }
    }
}
