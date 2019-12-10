using Core.Application.Dto.EditDto;
using Core.Application.Dto.ReturnDto;
using Core.Domain.Entities;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Core.Application.Controllers.Admin
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    public class MenuController : AdminBaseController
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMenuList()
        {
            var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role);
            var roleIds = roleClaim.Value.Split(',').Cast<int>();
            var service = GetInstance<ContollerActionRole>();
            var expression = new ExpressionSpecification<ContollerActionRole>(x => roleIds.Contains(x.RoleId));
            var contollerActionRoles = service.GetByCondition(expression, x => x.ControllerActionPermissions);
            var controllers = contollerActionRoles.Select(x => x.ControllerActionPermissions.ControllerPermissions);
            return JsonSuccess("查询成功!", controllers);
        }

        /// <summary>
        /// 编辑菜单页
        /// </summary>
        /// <returns></returns>
        public IActionResult EditMenu(int? id)
        {
            var menu = new MenuEditDto();
            if (id.HasValue && id > 0)
            {
                var service = GetInstance<ControllerPermissions>();
                menu = MapForm<ControllerPermissions, MenuEditDto>(service.Get(id.Value));
            }
            return View(menu);
        }

        /// <summary>
        /// 编辑菜单
        /// </summary>
        /// <param name="menuEditDto"></param>
        /// <returns></returns>
        public IActionResult EditMenu(MenuEditDto menuEditDto)
        {
            return AddDto<ControllerPermissions, MenuEditDto, MenuReturnDto>(menuEditDto);
        }
    }
}
