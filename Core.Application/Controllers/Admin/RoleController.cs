using Core.Application.Dto;
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
        /// 异步获取数据
        /// </summary>
        /// <returns></returns>
        [Initialize("获取数据", "", "", false)]
        public async Task<IActionResult> GetListAsync(BaseQueryPageDto baseQueryPageDto)
        {
            return await GetPageListAsync<Role, BaseQueryPageDto, Role>(baseQueryPageDto);
        }
    }
}
