using Microsoft.AspNetCore.Http;

namespace UEditorNetCore.Handlers
{
    /// <summary>
    /// δ֪��������
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
        /// ����
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