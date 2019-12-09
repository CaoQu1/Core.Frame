using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 网站信息实体
    /// </summary>
    public class CoreWebSite
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteName { get; set; }

        /// <summary>
        /// 网站域名
        /// </summary>
        public string SiteDomain { get; set; }

        /// <summary>
        /// 首页标题
        /// </summary>
        public string HomeTitle { get; set; }

        /// <summary>
        /// 版权信息
        /// </summary>
        public string CopyRight { get; set; }
    }
}
