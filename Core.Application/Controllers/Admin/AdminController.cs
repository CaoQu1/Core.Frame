using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Controllers.Admin
{
    /// <summary>
    /// 后台控制器
    /// </summary>
    public class AdminController : AdminBaseController
    {
        public AdminController()
        {
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
    }
}
