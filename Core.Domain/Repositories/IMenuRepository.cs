using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Repositories
{
    /// <summary>
    ///菜单数据接口
    /// </summary>
    public interface IMenuRepository : IRepository<int, ControllerPermissions>
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        List<ControllerPermissions> GetControllerPermissions(string[] roleIds);
    }
}
