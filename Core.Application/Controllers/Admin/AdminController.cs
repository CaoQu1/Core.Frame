using AutoMapper;
using Core.Application.Dto;
using Core.Domain;
using Core.Domain.Entities;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Core.Application.Dto.EditDto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Core.Application.Controllers.Admin
{
    /// <summary>
    /// 后台控制器
    /// </summary>
    public class AdminController : AdminBaseController
    {
        private readonly IVerifyCodeService _verifyCodeService;
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="verifyCodeService"></param>
        public AdminController(IVerifyCodeService verifyCodeService, IEncryptionService encryptionService)
        {
            this._verifyCodeService = verifyCodeService;
            this._encryptionService = encryptionService;
        }

        /// <summary>
        /// 登录页
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "")
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAuthCode()
        {
            var codeModel = this._verifyCodeService.GetVerifyCode();
            var seesionValue = this._encryptionService.MD5(codeModel.Item2);
            HttpContext.Session.Set(this._verifyCodeService.CodeKey, Encoding.Default.GetBytes(seesionValue));
            await HttpContext.Session.CommitAsync();
            return File(codeModel.Item1, @"image/Gif");
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CheckLogin(string username, string password, string code, string returnUrl)
        {
            try
            {
                var md5Code = this._encryptionService.MD5(code);
                HttpContext.Session.TryGetValue(this._verifyCodeService.CodeKey, out byte[] seesionCode);
                if (seesionCode.Length > 0 && md5Code.Equals(Encoding.Default.GetString(seesionCode)))
                {
                    var expressionSpecification = new ExpressionSpecification<SystemUser>(x => x.UserName == username);
                    var systemUser = SystemUserService.Instance.GetByCondition(expressionSpecification, x => x.SystemUserRoles).FirstOrDefault();
                    if (systemUser != null)
                    {
                        if (systemUser.PassWord.Equals(password))
                        {
                            var claimsPrincipal = new ClaimsPrincipal();
                            var claimsIdentity = new ClaimsIdentity();
                            claimsIdentity.AddClaims(new Claim[] {
                                new Claim(ClaimTypes.Sid, systemUser.Id.ToString()),
                                new Claim(ClaimTypes.Role,string.Join(',' ,systemUser.SystemUserRoles.Select(x=>x.RoleId).ToList()))
                            });
                            claimsPrincipal.AddIdentity(claimsIdentity);
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties
                            {
                                RedirectUri = returnUrl,
                                IsPersistent = true,
                                ExpiresUtc = DateTime.Now.AddMinutes(30)
                            });
                            return JsonSuccess<SystemUser, SystemUserEditDto>("登录成功!", systemUser);
                        }
                        else
                        {
                            return JsonFail("密码错误!");
                        }
                    }
                    else
                    {
                        return JsonFail("用户不存在!");
                    }
                }
                else
                {
                    return JsonFail("验证码错误!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public async Task OutLogin()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
