using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class MenuService : BaseDomainService<int, ControllerPermissions>
    {
        private readonly IRepository<int, ContollerActionRole> _controllerRoleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public MenuService(IRepository<int, ControllerPermissions> repository,
            IRepository<int, ContollerActionRole> controllerRoleRepository,
            ILogger<MenuService> logger) : base(repository, logger: logger)
        {
            _controllerRoleRepository = controllerRoleRepository;
        }

        /// <summary>
        /// 实例
        /// </summary>
        public static MenuService Instance => CoreAppContext.GetService<MenuService>();

        ///// <summary>
        ///// 获取菜单
        ///// </summary>
        ///// <param name="roleIds"></param>
        ///// <returns></returns>
        //public IList<ControllerPermissions> GetControllerPermissions(int )
        //{

        //}
    }
}
