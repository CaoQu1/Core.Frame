using Core.Global;
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
        public IActionResult Master()
        {
            return View(_coreWebSite);
        }
    }
}
