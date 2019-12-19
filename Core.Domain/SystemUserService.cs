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
        /// 用户数据仓库
        /// </summary>
        private readonly ISystemUserRepository _systemUserRepository;

        /// <summary>
        /// 菜单数据接口
        /// </summary>
        private readonly IMenuRepository _menuRepository;

        /// <summary>
        ///菜单角色权限操作
        /// </summary>
        private readonly IRepository<int, ActionRole> _actionRoleRepository;

        /// <summary>
        ///操作角色权限操作
        /// </summary>
        private readonly IRepository<int, ControllerPermissions> _controllerRepository;

        /// <summary>
        ///操作角色权限操作
        /// </summary>
        private readonly IRepository<int, ActionPermissions> _actionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public SystemUserService(ISystemUserRepository repository,
            IMenuRepository menuRepository,
            IRepository<int, ActionRole> actionRoleRepository,
            IRepository<int, ActionPermissions> actionRepository,
        IRepository<int, ControllerPermissions> controllerRepository,
            ILogger<SystemUserService> logger) : base(repository, logger: logger)
        {
            this._actionRoleRepository = actionRoleRepository;
            this._controllerRepository = controllerRepository;
            this._menuRepository = menuRepository;
            this._systemUserRepository = repository as ISystemUserRepository;
            this._actionRepository = actionRepository;
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
        public List<SystemUserRole> GetSystemUserRole(int userId)
        {
            return Invoke<List<SystemUserRole>>(() =>
            {
                return Get(userId, x => x.SystemUserRoles).SystemUserRoles.ToList();
            });
        }

        /// <summary>
        /// 获取角色菜单和操作
        /// </summary>
        /// <param name="systemUserRoles"></param>
        /// <returns></returns>
        public (List<ControllerPermissions>, List<ActionPermissions>) GetRolePermissions(IList<int> roleIds)
        {
            return Invoke<(List<ControllerPermissions>, List<ActionPermissions>)>(() =>
             {
                 var actionPermissions = (from roleId in roleIds
                                          join actionRole in this._actionRoleRepository.Table
                                          on roleId equals actionRole.RoleId
                                          join action in _actionRepository.Table
                                          on actionRole.ActionId equals action.Id
                                          select action).ToList();
                 return (this._controllerRepository.GetByCondition(new ExpressionSpecification<ControllerPermissions>(x => actionPermissions.Select(y => y.ControllerId).Contains(x.Id))).ToList(), actionPermissions);
             });
        }

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public List<ControllerPermissions> GetControllerPermissions(string[] roleIds)
        {
            return _menuRepository.GetControllerPermissions(roleIds);
        }
    }
}
