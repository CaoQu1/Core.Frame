using AutoMapper;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Service;
using Core.Global;
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
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// mapper
        /// </summary>
        protected readonly IMapper _mapper;

        /// <summary>
        /// ctor
        /// </summary>
        public BaseController()
        {
            this._mapper = CoreAppContext.GetService<IMapper>();
        }

        /// <summary>
        /// dto转实体
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual TEntity MapTo<TDto, TEntity>(TDto dto)
        {
            return this._mapper.Map<TEntity>(dto);
        }

        /// <summary>
        /// 实体转dto
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        protected virtual TDto MapForm<TEntity, TDto>(TEntity entity)
        {
            return this._mapper.Map<TDto>(entity);
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual IActionResult JsonSuccess<T>(string message, T value)
        {
            return Json(new CoreResult<T> { Success = true, Message = message, Value = value });
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual IActionResult JsonSuccess<T, TDto>(string message, T value)
        {
            return Json(new CoreResult<TDto> { Success = true, Message = message, Value = MapForm<T, TDto>(value) });
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual IActionResult JsonFail(string message)
        {
            return Json(new CoreResult { Success = false, Message = message });
        }
    }
}
