using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 用户领域服务
    /// </summary>
    public class SystemUserService : BaseDomainService<int, SystemUser>, ISystemUserService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public SystemUserService(IRepository<int, SystemUser> repository, ILogger<SystemUserService> logger) : base(repository, logger: logger)
        {
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="systemUser"></param>
        /// <returns></returns>
        public CoreResult Register(SystemUser systemUser)
        {
            return Invoke<CoreResult>(() =>
             {
                 CoreResult coreResult = new CoreResult();
                 coreResult.Success = this._repository.Add(systemUser) > 0;
                 coreResult.Message = "用户注册" + (coreResult.Success ? "成功!" : "失败");
                 return coreResult;
             });
        }
    }
}
