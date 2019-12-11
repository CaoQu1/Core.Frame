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
        /// 菜单
        /// </summary>
        private readonly IRepository<int, ControllerPermissions> _controllerRepository;

        /// <summary>
        /// 菜单操作
        /// </summary>
        private readonly IRepository<int, ControllerActionPermissions> _controllerActionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public SystemUserService(IRepository<int, SystemUser> repository,
            IRepository<int, ContollerActionRole> actionRoleRepository,
            IRepository<int, ControllerPermissions> controllerRepository,
            IRepository<int, ControllerActionPermissions> controllerActionRepository,
            ILogger<SystemUserService> logger) : base(repository, logger: logger)
        {
            this._actionRoleRepository = actionRoleRepository;
            this._controllerRepository = controllerRepository;
            this._controllerActionRepository = controllerActionRepository;
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static SystemUserService Instance => CoreAppContext.GetService<SystemUserService>();

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
        public CoreResult<List<ControllerActionPermissions>> GetRolePermissions(IList<SystemUserRole> systemUserRoles)
        {
            return Invoke<CoreResult<List<ControllerActionPermissions>>>(() =>
            {
                CoreResult<List<ControllerActionPermissions>> coreResult = new CoreResult<List<ControllerActionPermissions>>();
                var controllerActionIds = (from role in systemUserRoles
                                           join actionRole in this._actionRoleRepository.Table
                                           on role.RoleId equals actionRole.RoleId
                                           join controllerAction in this._controllerActionRepository.Table
                                           on actionRole.ControllerActionId equals controllerAction.Id
                                           select controllerAction.Id).ToList();

                coreResult.Value = this._controllerActionRepository.GetByCondition(new ExpressionSpecification<ControllerActionPermissions>(x => controllerActionIds.Contains(x.Id)), x => x.ControllerPermissions, x => x.ActionPermissions).ToList();
                coreResult.Message = "用户权限获取" + (coreResult.Success ? "成功!" : "失败");
                return coreResult;
            });
        }
    }
}
