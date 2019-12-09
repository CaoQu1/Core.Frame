using System.Collections.Generic;
using System.Linq;

namespace Core.Domain.Repositories
{
    /// <summary>
    /// 入参 分页筛选条件
    /// </summary>
    public class Pager
    {
        public static dynamic Paging(IEnumerable<dynamic> list, long total)
        {
            return new { code = 0, count = total, data = list };
        }

    }
    public class PageInfo
    {
        public int page { get; set; }
        public int limit { get; set; }
        /// <summary>
        /// 排序字段 CreateOn
        /// </summary>
        public string field { get; set; }
        /// <summary>
        /// 排序方式 asc desc
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 返回字段逗号分隔
        /// </summary>
        public string returnFields { get; set; }
        public string prefix { get; set; }
    }
}