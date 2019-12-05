using Core.Domain;
using Core.Domain.Entities;
using Core.Global;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Filter
{
    /// <summary>
    /// 授权筛选器
    /// </summary>
    public class CoreAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var controllerActionDescriptor = (context.ActionDescriptor as ControllerActionDescriptor);
            if (controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
            {
                return;
            }
            var userClaim = context.HttpContext.User.FindFirst(ClaimTypes.Sid);
            if (userClaim == null || userClaim.Value.IsEmpty())
            {
                AuthorizationFailResult(context);
                return;
            }
            var cacheManagerService = CoreAppContext.GetService<ICacheManagerService>();
            var systemUserRoles = await cacheManagerService.GetOrAdd<List<SystemUserRole>>(String.Format(CoreConst.USERROLES, userClaim.Value), () =>
               {
                   return SystemUserService.Instance.GetSystemUserRole(int.Parse(userClaim.Value)).Value;
               }, TimeSpan.FromMinutes(30));

            var controllerActionRoles = await cacheManagerService.GetOrAdd<List<ContollerActionRole>>(String.Format(CoreConst.USERROLEACTIONS, userClaim.Value), () =>
             {
                 return SystemUserService.Instance.GetRolePermissions(systemUserRoles).Value;
             }, TimeSpan.FromMinutes(30));

            var controller = context.RouteData.Values["Controller"].ToString();
            var action = context.RouteData.Values["Action"].ToString();

            if (!controllerActionRoles.Any(x => x.Controller == controller && x.Action == action))
            {
                AuthorizationFailResult(context);
                return;
            }
        }

        /// <summary>
        /// 判断请求类型
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static bool IsAjaxRequest(HttpRequest request)
        {
            return string.Equals(request.Query["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal) ||
                string.Equals(request.Headers["X-Requested-With"], "XMLHttpRequest", StringComparison.Ordinal);
        }

        /// <summary>
        /// 没有权限
        /// </summary>
        /// <param name="context"></param>
        private static void AuthorizationFailResult(AuthorizationFilterContext context)
        {
            if (IsAjaxRequest(context.HttpContext.Request))
            {
                context.Result = new JsonResult("未登录!")
                {
                    StatusCode = 401,
                };
            }
            else
            {
                context.Result = new RedirectResult("/admin/admin/login");
            }
        }
    }
}
