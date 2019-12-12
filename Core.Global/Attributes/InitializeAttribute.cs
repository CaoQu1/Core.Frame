using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global.Attributes
{
    /// <summary>
    /// 初始化功能
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InitializeAttribute : Attribute
    {
        public string FunctionName { get; set; }

        public string ModuleUrl { get; set; }

        public string Icon { get; set; }

        public int? SortId { get; set; }

        public bool IsShow { get; set; }

        public CoreEnum.Operation? Type { get; set; }

        public InitializeAttribute() { }

        public InitializeAttribute(string functionName, string moduleUrl = "", string icon = "", bool isShow = true)
        {
            FunctionName = functionName;
            ModuleUrl = moduleUrl;
            Icon = icon;
            IsShow = isShow;
        }
    }
}
