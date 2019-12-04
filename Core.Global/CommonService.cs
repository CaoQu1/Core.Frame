using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Global
{
    /// <summary>
    /// 基础服务类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonService<T>
    {
        /// <summary>
        /// 日志接口
        /// </summary>
        protected ILogger<T> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        public CommonService()
        {
            this._logger = CoreAppContext.GetService<ILogger<T>>();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        protected async Task<TEntity> Invoke<TEntity>(Func<Task<TEntity>> action)
        {
            try
            {
                return await action.Invoke();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
            }
            return await Task.FromResult<TEntity>(default(TEntity));
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="action"></param>
        protected async Task Invoke(Func<Task> action)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
            }

        }
    }
}
