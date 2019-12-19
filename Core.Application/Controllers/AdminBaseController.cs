using Core.Application.Dto;
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
using System.Reflection;
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
        /// 重写方法执行前
        /// </summary>
        /// <param name="context"></param>
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await Invoke(async () =>
           {
               //var controllerId = context.ActionArguments.ContainsKey("id") ? context.ActionArguments["id"]?.ToString() : null;
               //if (!controllerId.IsEmpty())
               //{
               //var controllerActionService = GetInstance<ControllerActionPermissions>();
               var controllerQueryService = GetInstance<PageAction>();
               var buttonList = controllerQueryService.Get().ToList();
               ViewData["buttonList"] = buttonList.Where(x => x.ActionType == CoreEnum.ActionType.Inside && x.ControlType == CoreEnum.ControlType.Button).ToList();
               ViewData["buttonOutList"] = buttonList.Where(x => x.ActionType == CoreEnum.ActionType.OutSide && x.ControlType == CoreEnum.ControlType.Button).ToList();
               ViewData["queryList"] = buttonList.Where(x => x.ControlType != CoreEnum.ControlType.Button && x.ActionType == CoreEnum.ActionType.OutSide);
               //}
               await base.OnActionExecutionAsync(context, next);
           });
        }

        /// <summary>
        /// 初始化权限
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult InitPermissions()
        {
            return Invoke<IActionResult>(() =>
           {
               var roleClaim = HttpContext.User.FindFirst(ClaimTypes.Role);
               var cacheManagerService = CoreAppContext.GetService<ICacheManagerService>();
               var roles = cacheManagerService.GetOrAdd<List<Role>>(String.Format(CoreConst.USERROLES, roleClaim.Value), () =>
               {
                   var roleIds = roleClaim.Value.Split(',').ToList();
                   return GetInstance<Role>().GetByCondition(new ExpressionSpecification<Role>(x => roleIds.Contains(x.Id.ToString()))).ToList();
               }, TimeSpan.FromMinutes(30)).Result;

               if (!roles.Any(x => x.IsSystemAdmin))
               {
                   return JsonFail("权限不足!");
               }

               var roleId = roles.Where(x => x.IsSystemAdmin).FirstOrDefault().Id;
               var controllerService = GetInstance<ControllerPermissions>();
               var actionService = GetInstance<ActionPermissions>();
               var controllerRoleService = GetInstance<ControllerRole>();
               var actionRoleServic = GetInstance<ActionRole>();
               var assembly = Assembly.GetExecutingAssembly();

               var controllerTypes = assembly.GetTypes().Where(x => !x.IsAbstract && typeof(Controller).IsAssignableFrom(x));
               if (controllerTypes.Count() > 0)
               {
                   var area = controllerTypes.First().Namespace.Split('.').Last();
                   foreach (Type controllerType in controllerTypes)
                   {
                       string name = string.Empty;
                       var controllerInitializeAttribute = controllerType.GetCustomAttribute(typeof(InitializeAttribute)) as InitializeAttribute;
                       if (controllerInitializeAttribute != null)
                       {
                           name = controllerInitializeAttribute.FunctionName;
                           area = controllerInitializeAttribute.Area;
                       }

                       if (controllerInitializeAttribute == null)
                       {
                           continue;
                       }

                       var controller = controllerType.Name.Replace("Controller", "");
                       var controllerPermissions = controllerService.GetByCondition(new ExpressionSpecification<ControllerPermissions>(x => x.Area == area && x.Controller == controller));
                       int controllerId = 0, actionId = 0;
                       if (controllerInitializeAttribute != null && controllerPermissions.Count == 0)
                       {
                           controllerId = controllerService.Add(new ControllerPermissions
                           {
                               ModuleName = name,
                               CreateTime = DateTime.Now,
                               Controller = controller,
                               ModuleUrl = controllerInitializeAttribute.ModuleUrl,
                               Area = area,
                               Icon = controllerInitializeAttribute.Icon,
                               SortId = controllerInitializeAttribute.SortId,
                               IsShow = controllerInitializeAttribute.IsShow
                           });
                       }

                       if (controllerPermissions.Count > 0)
                       {
                           controllerId = controllerPermissions.First().Id;
                       }

                       var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                       foreach (MethodInfo item in methods)
                       {
                           var actionAttributes = item.GetCustomAttribute(typeof(InitializeAttribute)) as InitializeAttribute;
                           if (actionAttributes != null)
                           {
                               name = actionAttributes.FunctionName;
                           }

                           if (actionAttributes == null)
                           {
                               continue;
                           }

                           var action = item.Name;
                           var actionPermissions = actionService.GetByCondition(new ExpressionSpecification<ActionPermissions>(x => x.Action == action && x.ControllerId == controllerId));
                           if (actionAttributes != null && actionPermissions.Count == 0)
                           {
                               actionId = actionService.Add(new ActionPermissions
                               {
                                   Action = action,
                                   ActionName = name,
                                   CreateTime = DateTime.Now,
                                   Icon = actionAttributes.Icon,
                                   SortId = actionAttributes.SortId,
                                   IsShow = actionAttributes.IsShow,
                                   ControllerId = controllerId
                               });
                           }

                           if (actionPermissions.Count > 0)
                           {
                               actionId = actionPermissions.First().Id;
                           }

                           if (controllerId != 0)
                           {
                               var controllerRoles = controllerRoleService.GetByCondition(new ExpressionSpecification<ControllerRole>(x => x.ControllerId == controllerId && x.RoleId == roleId));
                               if (controllerRoles.Count == 0)
                               {
                                   controllerRoleService.Add(new ControllerRole
                                   {
                                       CreateTime = DateTime.Now,
                                       RoleId = roleId,
                                       SortId = 1,
                                       SystemId = 1,
                                       ControllerId = controllerId,
                                   });
                               }
                           }

                           if (actionId != 0)
                           {
                               var controllerRoles = actionRoleServic.GetByCondition(new ExpressionSpecification<ActionRole>(x => x.ActionId == actionId && x.RoleId == roleId));
                               if (controllerRoles.Count == 0)
                               {
                                   actionRoleServic.Add(new ActionRole
                                   {
                                       CreateTime = DateTime.Now,
                                       RoleId = roleId,
                                       SortId = 1,
                                       SystemId = 1,
                                       ActionId = actionId,
                                       ControllerId = controllerId
                                   });
                               }
                           }
                       }
                   }
               }
               return JsonSuccess("初始化成功!");
           });
        }

        /// <summary>
        /// 列表页
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 编辑页
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit<TEntity, TDto>(int? id) where TEntity : class, IAggregateRoot<int>, new()
        {
            return Invoke<IActionResult>(() =>
            {
                TEntity entity = new TEntity();
                var tentityService = GetInstance<TEntity>();
                if (tentityService != null && id.HasValue && id.Value > 0)
                {
                    entity = tentityService.Get(id.Value);
                }
                return View(MapForm<TEntity, TDto>(entity));
            });
        }

        /// <summary>
        /// 编辑数据
        /// </summary>
        /// <returns></returns>
        public IActionResult Edit<TEntity, TDto, TResult>(TDto dto) where TEntity : class, IAggregateRoot<int>, new() where TDto : BaseDto
        {
            return dto.Id.HasValue && dto.Id.Value > 0 ? UpdateDto<TEntity, TDto, TResult>(dto) : AddDto<TEntity, TDto, TResult>(dto);
        }
    }
}
