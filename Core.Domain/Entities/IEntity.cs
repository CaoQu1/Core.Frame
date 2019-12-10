using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 实体接口
    /// </summary>
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 标识
        /// </summary>
        TKey Id { get; set; }

        /// <summary>
        /// 对值初始化
        /// </summary>
        void FormatInitValue();
    }

    /// <summary>
    /// 用户
    /// </summary>
    public interface IUserEntity<Tkey, TUser> : IEntity<Tkey>
    {
        /// <summary>
        /// 创建人ID
        /// </summary>
        int CreateUserId { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        int UpdateUserId { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        TUser CreateUser { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        TUser UpdateUser { get; set; }
    }
}
