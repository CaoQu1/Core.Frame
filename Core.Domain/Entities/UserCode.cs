using System;
using System.Collections.Generic;
using System.Text;
using static Core.Global.CoreEnum;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 用户验证码实体
    /// </summary>
    public class UserCode : AggregateRoot<UserCode, int>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 验证码类型
        /// </summary>
        public CodeType CodeType { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 失效时间
        /// </summary>
        public DateTime ExpiryTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
