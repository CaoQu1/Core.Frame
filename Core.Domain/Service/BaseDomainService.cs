﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Service
{
    public class BaseDomainService<TKey, TEntity> : IDomainService<TKey, TEntity> where TEntity : class, IAggregateRoot<TKey>
    {
        private readonly IRepository<TKey, TEntity> _repository;
        private readonly ILogger<BaseDomainService<TKey, TEntity>> _logger;

        public BaseDomainService(IRepository<TKey, TEntity> repository, ILogger<BaseDomainService<TKey, TEntity>> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }
    }
}