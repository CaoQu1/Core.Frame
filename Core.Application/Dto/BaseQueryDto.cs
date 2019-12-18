using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    /// <summary>
    /// 查询基础模型
    /// </summary>
    public class BaseQueryDto : BaseDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
