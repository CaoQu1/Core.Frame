using Core.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 用户角色实体
    /// </summary>
    public class SystemUserRole : AggregateRoot<SystemUserRole, int>
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int SystemUserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// 用户实体
        /// </summary>
        public virtual SystemUser SystemUser { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 配置数据库映射
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<SystemUserRole> builder)
        {
            builder.ToTable("SystemUserRoles");
            builder.HasOne(x => x.Role).WithMany(y => y.SystemUserRoles).HasForeignKey(f => f.RoleId);
            builder.HasOne(x => x.SystemUser).WithMany(y => y.SystemUserRoles).HasForeignKey(f => f.SystemUserId);

            builder.HasData(new SystemUserRole
            {
                RoleId = 1,
                SortId = 1,
                SystemId = 1,
                CreateTime = DateTime.Now,
                SystemUserId = 1,
                Id = 1
            });

            base.Configure(builder);
        }
    }
}
