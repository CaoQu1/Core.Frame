using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UEditorNetCore.Handlers;

namespace UEditorNetCore
{
    /// <summary>
    /// 上传服务集合
    /// </summary>
    public class UEditorActionCollection : Dictionary<string, Action<HttpContext>>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UEditorActionCollection()
        {
            Add("config", ConfigAction);
            Add("uploadimage", UploadImageAction);
            Add("uploadscrawl", UploadScrawlAction);
            Add("uploadvideo", UploadVideoAction);
            Add("uploadfile", UploadFileAction);
            Add("listimage", ListImageAction);
            Add("listfile", ListFileAction);
            Add("catchimage", CatchImageAction);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="action"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public new UEditorActionCollection Add(string action, Action<HttpContext> handler)
        {
            if (ContainsKey(action))
                this[action] = handler;
            else
                base.Add(action, handler);

            return this;
        }

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public new UEditorActionCollection Remove(string action)
        {
            base.Remove(action);
            return this;
        }

        /// <summary>
        /// 获取配置文件
        /// </summary>
        /// <param name="context"></param>
        private void ConfigAction(HttpContext context)
        {
            new ConfigHandler(context).Process();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="context"></param>
        private void UploadImageAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = Config.GetStringList("imageAllowFiles"),
                PathFormat = Config.GetString("imagePathFormat"),
                SizeLimit = Config.GetInt("imageMaxSize"),
                UploadFieldName = Config.GetString("imageFieldName")
            }).Process();
        }

        /// <summary>
        /// 抓取图片
        /// </summary>
        /// <param name="context"></param>
        private void UploadScrawlAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = new string[] { ".png" },
                PathFormat = Config.GetString("scrawlPathFormat"),
                SizeLimit = Config.GetInt("scrawlMaxSize"),
                UploadFieldName = Config.GetString("scrawlFieldName"),
                Base64 = true,
                Base64Filename = "scrawl.png"
            }).Process();
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        /// <param name="context"></param>
        private void UploadVideoAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = Config.GetStringList("videoAllowFiles"),
                PathFormat = Config.GetString("videoPathFormat"),
                SizeLimit = Config.GetInt("videoMaxSize"),
                UploadFieldName = Config.GetString("videoFieldName")
            }).Process();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="context"></param>
        private void UploadFileAction(HttpContext context)
        {
            new UploadHandler(context, new UploadConfig()
            {
                AllowExtensions = Config.GetStringList("fileAllowFiles"),
                PathFormat = Config.GetString("filePathFormat"),
                SizeLimit = Config.GetInt("fileMaxSize"),
                UploadFieldName = Config.GetString("fileFieldName")
            }).Process();
        }

        /// <summary>
        /// 图片列表
        /// </summary>
        /// <param name="context"></param>
        private void ListImageAction(HttpContext context)
        {
            new ListFileManager(
                    context,
                    Config.GetString("imageManagerListPath"),
                    Config.GetStringList("imageManagerAllowFiles"))
                .Process();
        }

        /// <summary>
        /// 文件列表
        /// </summary>
        /// <param name="context"></param>
        private void ListFileAction(HttpContext context)
        {
            new ListFileManager(
                    context,
                    Config.GetString("fileManagerListPath"),
                    Config.GetStringList("fileManagerAllowFiles"))
                .Process();
        }

        /// <summary>
        /// 抓取图片
        /// </summary>
        /// <param name="context"></param>
        private void CatchImageAction(HttpContext context)
        {
            new CrawlerHandler(context).Process();
        }
    }
}
