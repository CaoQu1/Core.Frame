﻿using Core.Application.Filter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Controllers
{

    /// <summary>
    /// 后台控制器基类
    /// </summary>
    [CoreAuthorizationFilter]
    public abstract class AdminBaseController : BaseController
    {

    }
}
