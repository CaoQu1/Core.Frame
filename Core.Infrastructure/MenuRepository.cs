using Core.Domain.Entities;
using Core.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.Infrastructure
{

    /// <summary>
    /// 菜单数据仓库
    /// </summary>
    public class MenuRepository : BaseRepository<int, ControllerPermissions>, IMenuRepository
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public MenuRepository(IDbContext dbContext, ILogger<MenuRepository> logger) : base(dbContext, logger)
        {
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public List<ControllerPermissions> GetControllerPermissions(string[] roleIds)
        {
            var menus = (from caRole in _dbContext.Set<ContollerActionRole>()
                         join caPermissions in _dbContext.Set<ControllerActionPermissions>()
                         on caRole.ControllerActionId equals caPermissions.Id
                         join cPermissions in Table
                         on caPermissions.ControllerId equals cPermissions.Id
                         where roleIds.Contains(caRole.RoleId.ToString())
                         select cPermissions).Distinct().ToList();
            return menus;
        }
    }
}
