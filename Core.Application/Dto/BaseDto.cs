using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    /// <summary>
    /// 基础模型
    /// </summary>
    public abstract class BaseDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 创建人ID
        /// </summary>
        public int? CreateUserId { get; set; }

        /// <summary>
        /// 修改人ID
        /// </summary>
        public int? UpdateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

    }
}
