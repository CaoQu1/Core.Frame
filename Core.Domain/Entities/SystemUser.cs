using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 用户领域模型
    /// </summary>
    public partial class SystemUser : AggregateRoot<SystemUser, int>
    {
        /// <summary>
        /// 配置数据库映射
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            throw new NotImplementedException();
        }
    }
}
