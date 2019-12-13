using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Core.Domain
{
    public class MenuService : BaseDomainService<int, ControllerPermissions>
    {
        private readonly IRepository<int, ContollerActionRole> _controllerRoleRepository;
        private readonly IRepository<int, ControllerActionPermissions> _controllerActionPermissionsRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public MenuService(IRepository<int, ControllerPermissions> repository,
            IRepository<int, ContollerActionRole> controllerRoleRepository,
            IRepository<int, ControllerActionPermissions> controllerActionPermissionsRepository,
            ILogger<MenuService> logger) : base(repository, logger: logger)
        {
            _controllerRoleRepository = controllerRoleRepository;
            _controllerActionPermissionsRepository = controllerActionPermissionsRepository;
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static MenuService Instance => CoreAppContext.GetService<MenuService>();

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public List<ControllerPermissions> GetControllerPermissions(string[] roleIds)
        {
            var menus = (from caRole in _controllerRoleRepository.Table
                         join caPermissions in _controllerActionPermissionsRepository.Table
                         on caRole.ControllerActionId equals caPermissions.Id
                         join cPermissions in _repository.Table
                         on caPermissions.ControllerId equals cPermissions.Id
                         where roleIds.Contains(caRole.RoleId.ToString())
                         select cPermissions).ToList();
            return menus;
        }
    }
}
