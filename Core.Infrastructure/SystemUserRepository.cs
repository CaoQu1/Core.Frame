using Core.Domain.Entities;
using Core.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure
{
    /// <summary>
    /// 用户数据仓库
    /// </summary>
    public class SystemUserRepository : BaseRepository<int, SystemUser>, ISystemUserRepository
    {

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
        public SystemUserRepository(IDbContext dbContext, ILogger<SystemUserRepository> logger) : base(dbContext, logger)
        {
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="systemUserRoles"></param>
        /// <returns></returns>
        public List<int> GetRolePermissions(IList<int> roleIds)
        {
            return (from id in roleIds
                    join actionRole in _dbContext.Set<ContollerActionRole>()
                    on id equals actionRole.RoleId
                    join controllerAction in _dbContext.Set<ControllerActionPermissions>()
                    on actionRole.ControllerActionId equals controllerAction.Id
                    select controllerAction.Id).ToList();
        }
    }
}
