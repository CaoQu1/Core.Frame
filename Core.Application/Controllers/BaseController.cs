using AutoMapper;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Controllers
{
    /// <summary>
    /// 应用接口基类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseController : Controller
    {
        protected readonly IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public virtual TEntity MapTo<TDto, TEntity>(TDto dto)
        {
            return this._mapper.Map<TEntity>(dto);
        }

        public virtual TDto MapForm<TEntity, TDto>(TEntity entity)
        {
            return this._mapper.Map<TDto>(entity);
        }
    }
}
