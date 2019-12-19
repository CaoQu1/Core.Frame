using Core.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 操作实体
    /// </summary>
    public class ActionPermissions : AggregateRoot<ActionPermissions, int>
    {
        /// <summary>
        /// 控制器编号
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 控制器信息
        /// </summary>
        public virtual ControllerPermissions ControllerPermissions { get; set; }

        /// <summary>
        /// 操作权限信息
        /// </summary>
        public virtual ICollection<ActionRole> ActionRoles { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ActionPermissions> builder)
        {
            builder.ToTable("ActionPermissions");

            builder.HasOne(x => x.ControllerPermissions).WithMany(y => y.ActionPermissions).HasForeignKey(fk => fk.ControllerId);

            builder.HasData(new ActionPermissions
            {
                ActionName = "首页",
                CreateTime = DateTime.Now,
                Icon = "layui-icon-file-b",
                SortId = 1,
                Action = "Master",
                Id = 1,
                IsShow = false,
                ControllerId = 1
            });

            builder.HasData(new ActionPermissions
            {
                ActionName = "获取菜单",
                CreateTime = DateTime.Now,
                Icon = "layui-icon-file-b",
                SortId = 2,
                Action = "GetMenuList",
                Id = 2,
                IsShow = false,
                ControllerId = 2
            });

            base.Configure(builder);
        }
    }
}
