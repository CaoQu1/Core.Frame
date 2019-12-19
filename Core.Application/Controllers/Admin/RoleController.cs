using Core.Application.Dto;
using Core.Application.Dto.EditDto;
using Core.Domain.Entities;
using Core.Global.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Controllers.Admin
{
    [Initialize("角色管理", "/Admin/Role/Index", Area = "Admin")]
    public class RoleController : AdminBaseController
    {
        /// <summary>
        /// 角色列表页
        /// </summary>
        /// <returns></returns>
        [Initialize("角色列表页")]
        public override IActionResult Index()
        {
            return base.Index();
        }

        /// <summary>
        /// 异步获取数据
        /// </summary>
        /// <returns></returns>
        [Initialize("获取分页角色数据", "", "", false)]
        public async Task<IActionResult> GetListAsync(BaseQueryPageDto baseQueryPageDto)
        {
            return await GetPageListAsync<Role, BaseQueryPageDto, Role>(baseQueryPageDto);
        }

        /// <summary>
        /// 编辑角色页
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        [Initialize("编辑角色页")]
        public IActionResult Edit(int? id)
        {
            return Edit<Role, RoleEditDto>(id);
        }

        /// <summary>
        /// 保存角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [Initialize("保存角色")]
        public IActionResult Edit(RoleEditDto role)
        {
            return Edit<Role, RoleEditDto, Role>(role);
        }

    }
}
