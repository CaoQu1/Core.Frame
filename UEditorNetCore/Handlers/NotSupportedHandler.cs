using Microsoft.AspNetCore.Http;

namespace UEditorNetCore.Handlers
{
    /// <summary>
    /// 未知处理类型
    /// </summary>
    public class NotSupportedHandler : Handler
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public NotSupportedHandler(HttpContext context)
            : base(context)
        {
        }

        /// <summary>
        /// 处理
        /// </summary>
        public override void Process()
        {
            WriteJson(new
            {
                state = "action is empty or action not supperted."
            });
        }
    }
}