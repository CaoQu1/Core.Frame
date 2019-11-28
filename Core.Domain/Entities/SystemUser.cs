using System;
using System.Collections.Generic;
using System.Text;
using Core.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 系统用户领域模型
    /// </summary>
    public class SystemUser : AggregateRoot<SystemUser, int>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public CoreEnum.Sex Sex { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPortrait { get; set; }

        /// <summary>
        /// 注册地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 配置数据库映射
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.ToTable("SystemUsers");
            builder.Property(x => x.NickName).HasColumnName(nameof(NickName));
            builder.Property(x => x.HeadPortrait).HasColumnName(nameof(HeadPortrait));
            builder.Property(x => x.PassWord).HasColumnName(nameof(PassWord));
            builder.Property(x => x.Address).HasColumnName(nameof(Address));
            builder.Property(x => x.RegTime).HasColumnName(nameof(RegTime));
            builder.Property(x => x.UserName).HasColumnName(nameof(UserName));
            builder.Property(x => x.Mobile).HasColumnName(nameof(Mobile));
            builder.Property(x => x.Sex).HasColumnName(nameof(Sex));
        }
    }
}
