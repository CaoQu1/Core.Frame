using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 附件上传
    /// </summary>
    public class CoreUpload
    {
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 上传的服务器
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 允许上传的扩展名（多个逗号隔开）
        /// </summary>
        public string Ext { get; set; }

        /// <summary>
        /// 允许上传的大小
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 应用ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 应用Secret
        /// </summary>
        public string AppSercret { get; set; }
    }
}
