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
        /// 操作名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public CoreEnum.Operation? Type { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? ShowOrder { get; set; }

        /// <summary>
        /// 控制器操作关联信息
        /// </summary>
        public virtual ICollection<ControllerActionPermissions> ControllerActionPermissions { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ActionPermissions> builder)
        {
            builder.ToTable("ActionPermissions");
            base.Configure(builder);
        }
    }
}
