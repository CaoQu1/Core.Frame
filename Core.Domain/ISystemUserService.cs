using Core.Domain.Entities;
using Core.Domain.Service;
using Core.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    /// <summary>
    /// 用户领域服务
    /// </summary>
    public interface ISystemUserService : IDomainService<int, SystemUser>
    {
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="systemUser"></param>
        /// <returns></returns>
        CoreResult Register(SystemUser systemUser);
    }
}
