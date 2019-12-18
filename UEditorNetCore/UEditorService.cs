using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UEditorNetCore.Handlers;
using Microsoft.AspNetCore.Hosting;

namespace UEditorNetCore
{
    /// <summary>
    /// 编辑器上传服务
    /// </summary>
    public class UEditorService
    {
        /// <summary>
        /// 上传类型服务集合
        /// </summary>
        private UEditorActionCollection actionList;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="env"></param>
        /// <param name="actions"></param>
        public UEditorService(IHostingEnvironment env, UEditorActionCollection actions)
        {
            Config.WebRootPath = env.WebRootPath;
            actionList = actions;
        }

        /// <summary>
        /// 执行上传
        /// </summary>
        /// <param name="context"></param>
        public void DoAction(HttpContext context)
        {
            var action = context.Request.Query["action"];
            if (actionList.ContainsKey(action))
                actionList[action].Invoke(context);
            else
                new NotSupportedHandler(context).Process();
        }
    }
}
