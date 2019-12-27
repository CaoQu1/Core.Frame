using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    public class CoreConnection
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// DBType
        /// </summary>
        public CoreEnum.DBType DBType { get; set; }
    }
}
