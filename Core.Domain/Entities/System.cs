using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 系统实体
    /// </summary>
    public class System : AggregateRoot<System, int>
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }

        /// <summary>
        /// 主页路径
        /// </summary>
        public string HomeUrl { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<System> builder)
        {
            builder.ToTable("Systems");
            builder.HasData(new System
            {
                Id = 1,
                SystemName = "积分系统",
                HomeUrl = "Home/Index"
            });
            base.Configure(builder);
        }
    }
}
