using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UEditorNetCore;

namespace Core.Application.Controllers.Admin
{
    /// <summary>
    /// 编辑器控制器
    /// </summary>
    [Route("api/[controller]")]
    public class UEditorController : BaseController
    {
        /// <summary>
        /// 上传服务
        /// </summary>
        private UEditorService _uEditorService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="uEditorService"></param>
        public UEditorController(UEditorService uEditorService)
        {
            this._uEditorService = uEditorService;
        }

        /// <summary>
        /// 上传
        /// </summary>
        public void Upload()
        {
            _uEditorService.DoAction(HttpContext);
        }
    }
}
