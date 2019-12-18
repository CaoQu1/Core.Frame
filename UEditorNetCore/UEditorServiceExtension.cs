using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace UEditorNetCore
{
    /// <summary>
    /// 服务扩展
    /// </summary>
    public static class UEditorServiceExtension
    {
        /// <summary>
        /// 添加ue
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configFile"></param>
        /// <param name="isCache"></param>
        /// <returns></returns>
        public static UEditorActionCollection AddUEditorService(
            this IServiceCollection services, 
            string configFile="config.json", 
            bool isCache = false)
        {
            Config.ConfigFile = configFile;
            Config.noCache = !isCache;

            var actions = new UEditorActionCollection();
            services.AddSingleton(actions);
            services.AddSingleton<UEditorService>();

            return actions;
        }
    }
}
