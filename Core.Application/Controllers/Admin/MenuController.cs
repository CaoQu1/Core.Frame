﻿using Core.Application.Dto.EditDto;
using Core.Application.Dto.ReturnDto;
using Core.Domain;
using Core.Domain.Entities;
using Core.Global;
using Core.Global.Attributes;
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
    [Initialize(" 菜单", IsShow = false, Area = "Admin", ModuleUrl = "/Admin/Menu/Index")]
    public class MenuController : AdminBaseController
    {
        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/{area}/{controller}/{action}")]
        [Initialize(" 获取菜单数据")]
        public IActionResult GetMenuList()
        {
            var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role);
            var roleIds = roleClaim.Value.Split(',').ToArray();
            var menus = MapForm<List<ControllerPermissions>, List<MenuReturnDto>>(SystemUserService.Instance.GetControllerPermissions(roleIds));
            return JsonSuccess("查询成功!", menus);
        }

        /// <summary>
        /// 编辑菜单页
        /// </summary>
        /// <returns></returns>
        [Initialize(" 编辑菜单页")]
        public IActionResult EditMenu(int? id)
        {
            return Edit<ControllerPermissions, MenuEditDto>(id);
        }

        /// <summary>
        /// 保存菜单
        /// </summary>
        /// <param name="menuEditDto"></param>
        /// <returns></returns>
        [Initialize(" 保存菜单")]
        public IActionResult EditMenu(MenuEditDto menuEditDto)
        {
            return Edit<ControllerPermissions, MenuEditDto, MenuReturnDto>(menuEditDto);
        }
    }
}
