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
        /// 菜单操作
        /// </summary>
        private readonly IRepository<int, ControllerActionPermissions> _controllerActionRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public SystemUserService(ISystemUserRepository repository,
            IMenuRepository menuRepository,
            IRepository<int, ControllerActionPermissions> controllerActionRepository,
            ILogger<SystemUserService> logger) : base(repository, logger: logger)
        {
            this._controllerActionRepository = controllerActionRepository;
            this._menuRepository = menuRepository;
            this._systemUserRepository = repository as ISystemUserRepository;
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
        public List<ControllerActionPermissions> GetRolePermissions(IList<int> roleIds)
        {
            return Invoke<List<ControllerActionPermissions>>(() =>
            {
                var controllerActionIds = _systemUserRepository.GetRolePermissions(roleIds);
                return this._controllerActionRepository.GetByCondition(new ExpressionSpecification<ControllerActionPermissions>(x => controllerActionIds.Contains(x.Id)), x => x.ControllerPermissions, x => x.ActionPermissions).ToList();
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
