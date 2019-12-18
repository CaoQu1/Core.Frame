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
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortId { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool? IsDeleted { get; set; }

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
