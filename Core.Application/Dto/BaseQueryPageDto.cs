using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto
{
    /// <summary>
    /// 分页基础查询模型
    /// </summary>
    public class BaseQueryPageDto : BaseQueryDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
    }
}
