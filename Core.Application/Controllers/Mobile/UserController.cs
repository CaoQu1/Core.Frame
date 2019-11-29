using AutoMapper;
using Core.Application.Controllers;
using Core.Domain;
using Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="systemUserService"></param>
        /// <param name="mapper"></param>
        public UserController()
        {
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            return View();
        }
    }
}
