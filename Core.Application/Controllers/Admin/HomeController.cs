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
    [Initialize("主页", "/Admin/Home/Index", Area = "Admin")]
    public class HomeController : AdminBaseController
    {
        private readonly CoreWebSite _coreWebSite;

        public HomeController(IOptions<CoreWebSite> options)
        {
            _coreWebSite = options.Value;
        }

        [Initialize("列表页")]
        public override IActionResult Index()
        {
            return base.Index();
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
