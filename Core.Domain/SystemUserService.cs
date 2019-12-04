using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain
{
    /// <summary>
    /// 用户领域服务
    /// </summary>
    public class SystemUserService : BaseDomainService<int, SystemUser>
    {
        /// <summary>
        /// 角色
        /// </summary>
        private readonly IRepository<int, ContollerActionRole> _actionRoleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public SystemUserService(IRepository<int, SystemUser> repository,
            IRepository<int, ContollerActionRole> actionRoleRepository,
            ILogger<SystemUserService> logger) : base(repository, logger: logger)
        {
            this._actionRoleRepository = actionRoleRepository;
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static SystemUserService Instance => CoreAppContext.GetService<SystemUserService>();

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="systemUser"></param>
        /// <returns></returns>
        public CoreResult AddSystemUser(SystemUser systemUser)
        {
            return Invoke<CoreResult>(() =>
             {
                 CoreResult coreResult = new CoreResult();
                 coreResult.Success = Add(systemUser) > 0;
                 coreResult.Message = "用户添加" + (coreResult.Success ? "成功!" : "失败");
                 return coreResult;
             });
        }

        /// <summary>
        /// 获取用户角色
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CoreResult<List<SystemUserRole>> GetSystemUserRole(int userId)
        {
            return Invoke<CoreResult<List<SystemUserRole>>>(() =>
            {
                CoreResult<List<SystemUserRole>> coreResult = new CoreResult<List<SystemUserRole>>();
                coreResult.Value = Get(userId, x => x.SystemUserRoles).SystemUserRoles.ToList();
                coreResult.Message = "用户获取" + (coreResult.Success ? "成功!" : "失败");
                return coreResult;
            });
        }

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="systemUserRoles"></param>
        /// <returns></returns>
        public CoreResult<List<ContollerActionRole>> GetRolePermissions(IList<SystemUserRole> systemUserRoles)
        {
            return Invoke<CoreResult<List<ContollerActionRole>>>(() =>
            {
                CoreResult<List<ContollerActionRole>> coreResult = new CoreResult<List<ContollerActionRole>>();
                coreResult.Value = (from role in systemUserRoles
                                    join action in this._actionRoleRepository.Table
                                    on role.RoleId equals action.RoleId
                                    select action).ToList();
                coreResult.Message = "用户权限获取" + (coreResult.Success ? "成功!" : "失败");
                return coreResult;
            });
        }
    }
}
