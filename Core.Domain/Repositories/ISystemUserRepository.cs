using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Repositories
{
    /// <summary>
    /// 用户数据接口
    /// </summary>
    public interface ISystemUserRepository : IRepository<int, SystemUser>
    {
        /// <summary>
        /// 获取校色菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        List<int> GetRolePermissions(IList<int> roleIds);
    }
}
