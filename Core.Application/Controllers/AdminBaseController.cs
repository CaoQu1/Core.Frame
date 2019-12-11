using Core.Application.Filter;
using Core.Domain;
using Core.Domain.Entities;
using Core.Global;
using Core.Global.Attributes;
using Core.Global.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Controllers
{

    /// <summary>
    /// 后台控制器基类
    /// </summary>
    [CoreAuthorizationFilter]
    public abstract class AdminBaseController : BaseController
    {
        /// <summary>
        /// 初始化菜单
        /// </summary>
        /// <param name="context"></param>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            if (controllerActionDescriptor != null)
            {
                var initAttributes = controllerActionDescriptor.GetType().GetCustomAttributes(typeof(InitializeAttribute), true);
                var userClaim = context.HttpContext.User.FindFirst(ClaimTypes.Sid);
                var cacheManagerService = CoreAppContext.GetService<ICacheManagerService>();
                var systemUserRoles = await cacheManagerService.GetOrAdd<List<SystemUserRole>>(String.Format(CoreConst.USERROLES, userClaim.Value), () =>
                {
                    return SystemUserService.Instance.GetSystemUserRole(int.Parse(userClaim.Value)).Value;
                }, TimeSpan.FromMinutes(30));

                var controllerService = GetInstance<ControllerPermissions>();
                var actionService = GetInstance<ActionPermissions>();
                var controllerActionService = GetInstance<ControllerActionPermissions>();
                var controllerActionRoleService = GetInstance<ContollerActionRole>();
                string controllerName = string.Empty;
                InitializeAttribute initializeAttribute = null;
                if (initAttributes != null && initAttributes.Length > 0)
                {
                    initializeAttribute = initAttributes[0] as InitializeAttribute;
                    controllerName = initializeAttribute?.FunctionName;
                }

                var area = context.RouteData.Values["Area"].ToString();
                var controller = context.RouteData.Values["Controller"].ToString();
                var action = context.RouteData.Values["Action"].ToString();
                var controllerPermissions = controllerService.GetByCondition(new ExpressionSpecification<ControllerPermissions>(x => x.Area == area && x.Controller == controller));
                int controllerId = 0;
                if (initializeAttribute != null && controllerPermissions.Count == 0)
                {
                    controllerId = controllerService.Add(new ControllerPermissions
                    {
                        ModuleName = controllerName,
                        CreateTime = DateTime.Now,
                        Controller = controller,
                        ModuleUrl = initializeAttribute.ModuleUrl,
                        Area = area,
                        Icon = initializeAttribute.Icon,
                        SortId = initializeAttribute.SortId,
                        IsShow = true
                    });
                }
                else
                {
                    controllerId = controllerPermissions.First().Id;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
