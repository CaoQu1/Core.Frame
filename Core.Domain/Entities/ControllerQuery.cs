using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 页面查询条件
    /// </summary>
    public class ControllerQuery : AggregateRoot<ControllerQuery, int>
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// 查询编号
        /// </summary>
        public int QueryId { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 菜单信息
        /// </summary>
        public virtual ControllerPermissions ControllerPermissions { get; set; }

        /// <summary>
        /// 查询信息
        /// </summary>
        public virtual Query Query { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ControllerQuery> builder)
        {
            builder.ToTable("ControllerQueries");
            builder.HasOne(x => x.Query).WithMany(y => y.ControllerQueries).HasForeignKey(fk => fk.QueryId);
            builder.HasOne(x => x.ControllerPermissions).WithMany(y => y.ControllerQueries).HasForeignKey(fk => fk.ControllerId);
            base.Configure(builder);
        }
    }
}
