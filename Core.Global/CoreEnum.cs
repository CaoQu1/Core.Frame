using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 系统枚举
    /// </summary>
    public class CoreEnum
    {
        /// <summary>
        /// 排序
        /// </summary>
        public enum SortOrder
        {
            /// <summary>
            ///倒序
            /// </summary>
            Desc,

            /// <summary>
            /// 正序
            /// </summary>
            Asc
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// 男
            /// </summary>
            Man,

            /// <summary>
            /// 女
            /// </summary>
            WoMan
        }

        /// <summary>
        /// 状态
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// 启用
            /// </summary>
            Enable,

            /// <summary>
            ///禁用
            /// </summary>
            Disenable
        }

        /// <summary>
        /// 操作
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// 增加
            /// </summary>
            Add,

            /// <summary>
            /// 删除
            /// </summary>
            Delete,

            /// <summary>
            /// 查询
            /// </summary>
            Query,

            /// <summary>
            /// 更新
            /// </summary>
            Update,

            /// <summary>
            /// 审核
            /// </summary>
            Approve
        }
    }
}
