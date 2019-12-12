using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 控制器操作实体
    /// </summary>
    public class ControllerActionPermissions : AggregateRoot<ControllerActionPermissions, int>
    {
        /// <summary>
        /// 操作编号
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// 控制器编号
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// 控制器实体
        /// </summary>
        public virtual ControllerPermissions ControllerPermissions { get; set; }

        /// <summary>
        /// 操作实体
        /// </summary>
        public virtual ActionPermissions ActionPermissions { get; set; }

        /// <summary>
        /// 控制器操作角色关联信息
        /// </summary>
        public virtual ICollection<ContollerActionRole> ContollerActionRoles { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ControllerActionPermissions> builder)
        {
            builder.ToTable("ControllerActionPermissions");
            builder.HasOne(x => x.ControllerPermissions).WithMany(x => x.ControllerActionPermissions).HasForeignKey(f => f.ControllerId);
            builder.HasOne(x => x.ActionPermissions).WithMany(x => x.ControllerActionPermissions).HasForeignKey(f => f.ActionId);

            builder.HasData(new ControllerActionPermissions
            {
                CreateTime = DateTime.Now,
                SortId = 1,
                ActionId = 1,
                SystemId = 1,
                ControllerId = 1,
                Id = 1
            });

            builder.HasData(new ControllerActionPermissions
            {
                CreateTime = DateTime.Now,
                SortId = 2,
                ActionId = 2,
                SystemId = 1,
                ControllerId = 2,
                Id = 2
            });

            base.Configure(builder);
        }
    }
}
