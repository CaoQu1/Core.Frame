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
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAuthCode()
        {
            var codeModel = this._verifyCodeService.GetVerifyCode();
            var seesionValue = this._encryptionService.MD5(codeModel.Item2);
            HttpContext.Session.Set(this._verifyCodeService.CodeKey, Encoding.Default.GetBytes(seesionValue));
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
        public IActionResult CheckLogin(string username, string password, string code)
        {
            try
            {
                var md5Code = this._encryptionService.MD5(code);
                HttpContext.Session.TryGetValue(this._verifyCodeService.CodeKey, out byte[] seesionCode);
                if (seesionCode.Length > 0 && md5Code.Equals(Encoding.Default.GetString(seesionCode)))
                {
                    var expressionSpecification = new ExpressionSpecification<SystemUser>(x => x.UserName == username);
                    var systemUser = SystemUserService.Instance.GetByCondition(expressionSpecification).FirstOrDefault();
                    if (systemUser != null)
                    {
                        if (systemUser.PassWord.Equals(this._encryptionService.MD5(password)))
                        {
                            return JsonSuccess<SystemUser, SystemUserDto>("登录成功!", systemUser);
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
    }
}
