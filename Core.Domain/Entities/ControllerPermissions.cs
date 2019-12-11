using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 控制器实体
    /// </summary>
    public class ControllerPermissions : AggregateRoot<ControllerPermissions, int>
    {
        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 模块地址（首页地址）
        /// </summary>
        public string ModuleUrl { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 父模块
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 操作实体
        /// </summary>
        public virtual ICollection<ControllerActionPermissions> ControllerActionPermissions { get; set; }

        /// <summary>
        /// 父菜单
        /// </summary>
        public virtual ControllerPermissions ParentControllerPermissions { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public virtual ICollection<ControllerPermissions> ChildrenControllerPermissions { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ControllerPermissions> builder)
        {
            builder.ToTable("ControllerPermissions");
            builder.HasOne(x => x.ParentControllerPermissions).WithMany(y => y.ChildrenControllerPermissions).HasForeignKey(f => f.ParentId);

            builder.HasData(new ControllerPermissions
            {
                ModuleName = "主页",
                CreateTime = DateTime.Now,
                Area = "Admin",
                Controller = "Home",
                ModuleUrl = "Admin/Home/Index",
                Icon = "layui-icon-home",
                IsShow = true,
                SortId = 1,
                Id = 1
            });

            base.Configure(builder);
        }
    }
}
