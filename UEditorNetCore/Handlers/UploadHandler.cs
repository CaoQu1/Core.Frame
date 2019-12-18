using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace UEditorNetCore.Handlers
{
    /// <summary>
    /// 上传处理
    /// </summary>
    public class UploadHandler : Handler
    {
        /// <summary>
        /// 上传配置 
        /// </summary>
        public UploadConfig UploadConfig { get; private set; }

        /// <summary>
        /// 上传结果
        /// </summary>
        public UploadResult Result { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="config"></param>
        public UploadHandler(HttpContext context, UploadConfig config)
            : base(context)
        {
            this.UploadConfig = config;
            this.Result = new UploadResult() { State = UploadState.Unknown };
        }

        /// <summary>
        /// 执行
        /// </summary>
        public override void Process()
        {
            byte[] uploadFileBytes = null;
            string uploadFileName = null;

            if (UploadConfig.Base64)
            {
                uploadFileName = UploadConfig.Base64Filename;
                uploadFileBytes = Convert.FromBase64String(Request.Form[UploadConfig.UploadFieldName]);
            }
            else
            {
                var file = Request.Form.Files[UploadConfig.UploadFieldName];
                uploadFileName = file.FileName;

                if (!CheckFileType(uploadFileName))
                {
                    Result.State = UploadState.TypeNotAllow;
                    WriteResult();
                    return;
                }
                if (!CheckFileSize((int)file.Length))
                {
                    Result.State = UploadState.SizeLimitExceed;
                    WriteResult();
                    return;
                }

                uploadFileBytes = new byte[file.Length];
                try
                {
                    file.OpenReadStream().Read(uploadFileBytes, 0, (int)file.Length);
                }
                catch (Exception)
                {
                    Result.State = UploadState.NetworkError;
                    WriteResult();
                }
            }

            Result.OriginFileName = uploadFileName;

            var savePath = PathFormatter.Format(uploadFileName, UploadConfig.PathFormat);
            var localPath = Path.Combine(Config.WebRootPath, savePath);
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                }
                File.WriteAllBytes(localPath, uploadFileBytes);
                Result.Url = savePath;
                Result.State = UploadState.Success;
            }
            catch (Exception e)
            {
                Result.State = UploadState.FileAccessError;
                Result.ErrorMessage = e.Message;
            }
            finally
            {
                WriteResult();
            }
        }

        /// <summary>
        /// 输出
        /// </summary>
        private void WriteResult()
        {
            this.WriteJson(new
            {
                state = GetStateMessage(Result.State),
                url = Result.Url,
                title = Result.OriginFileName,
                original = Result.OriginFileName,
                error = Result.ErrorMessage
            });
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        /// <summary>
        /// 检查文件类型
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private bool CheckFileType(string filename)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return UploadConfig.AllowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        /// <summary>
        /// 检查文件大小
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private bool CheckFileSize(int size)
        {
            return size < UploadConfig.SizeLimit;
        }
    }

    /// <summary>
    /// 上传配置
    /// </summary>
    public class UploadConfig
    {
        /// <summary>
        /// 文件命名规则
        /// </summary>
        public string PathFormat { get; set; }

        /// <summary>
        /// 上传表单域名称
        /// </summary>
        public string UploadFieldName { get; set; }

        /// <summary>
        /// 上传大小限制
        /// </summary>
        public int SizeLimit { get; set; }

        /// <summary>
        /// 上传允许的文件格式
        /// </summary>
        public string[] AllowExtensions { get; set; }

        /// <summary>
        /// 文件是否以 Base64 的形式上传
        /// </summary>
        public bool Base64 { get; set; }

        /// <summary>
        /// Base64 字符串所表示的文件名
        /// </summary>
        public string Base64Filename { get; set; }
    }

    /// <summary>
    /// 上传结果
    /// </summary>
    public class UploadResult
    {
        /// <summary>
        /// 状态
        /// </summary>
        public UploadState State { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 源文件名
        /// </summary>
        public string OriginFileName { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }

    /// <summary>
    /// 上传状态
    /// </summary>
    public enum UploadState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 文件大小超出服务器限制
        /// </summary>
        SizeLimitExceed = -1,

        /// <summary>
        /// 不允许的文件格式
        /// </summary>
        TypeNotAllow = -2,

        /// <summary>
        /// 文件访问出错，请检查写入权限
        /// </summary>
        FileAccessError = -3,

        /// <summary>
        /// 网络错误
        /// </summary>
        NetworkError = -4,

        /// <summary>
        /// 未知错误
        /// </summary>
        Unknown = 1,
    }
}