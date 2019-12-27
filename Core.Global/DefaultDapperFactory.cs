using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using Microsoft.Extensions.Options;

namespace Core.Global
{
    public interface IDapperFactory
    {
        DapperClient CreateClient(string name);
    }

    public class DefaultDapperFactory : IDapperFactory
    {
        private IOptionsMonitor<CoreConnection> _optionsSnapshot;

        public DefaultDapperFactory(IOptionsMonitor<CoreConnection> optionsSnapshot)
        {
            _optionsSnapshot = optionsSnapshot;
        }

        public DapperClient CreateClient(string name)
        {
            return new DapperClient(_optionsSnapshot.Get(name));
        }
    }


    /// <summary>
    /// dapper帮助对象
    /// </summary>
    public class DapperClient
    {
        /// <summary>
        /// 数据库连接
        /// </summary>
        private CoreConnection _connection;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="connection"></param>
        public DapperClient(CoreConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// 获取数据库连接对象
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetDbConnection()
        {
            IDbConnection dbConnection = null;
            switch (_connection.DBType)
            {
                case CoreEnum.DBType.Oracle:
                    break;
                case CoreEnum.DBType.SqlServer:
                    dbConnection = new SqlConnection(_connection.ConnectionString);
                    break;
                default:
                    break;
            }
            return dbConnection;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public List<T> Query<T>(string sql)
        {
            using (var conn = GetDbConnection())
            {
                return conn.Query<T>(sql).AsList();
            }
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<T> Query<T>(string sql, object parameters)
        {
            using (var conn = GetDbConnection())
            {
                return conn.Query<T>(sql, parameters).AsList();
            }
        }
    }
}
