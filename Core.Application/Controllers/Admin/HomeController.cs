using Core.Global;
using Core.Global.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Controllers.Admin
{
    /// <summary>
    /// 主页控制器
    /// </summary>
    [Initialize("主页", "/Admin/Home/Index")]
    public class HomeController : AdminBaseController
    {
        private readonly CoreWebSite _coreWebSite;

        public HomeController(IOptions<CoreWebSite> options)
        {
            _coreWebSite = options.Value;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Initialize("首页")]
        public IActionResult Master()
        {
            return View(_coreWebSite);
        }

        /// <summary>
        /// 网站
        /// </summary>
        /// <returns></returns>
        [Initialize("网站")]
        public IActionResult Main()
        {
            return View();
        }

        /// <summary>
        /// 错误页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Error()
        {
            ViewBag.ex = HttpContext.Items["Exception"] as Exception;
            return View();
        }
    }
}
