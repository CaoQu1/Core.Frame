using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain
{
    public class GoodsService : BaseDomainService<int, Goods>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public GoodsService(IRepository<int, Goods> repository,
            ILogger<GoodsService> logger) : base(repository, logger: logger)
        {

        }

        /// <summary>
        /// 实例
        /// </summary>
        public static GoodsService Instance => CoreAppContext.GetService<GoodsService>();
    }
}
