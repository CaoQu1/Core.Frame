using AutoMapper;
using Core.Application.Dto;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Service;
using Core.Global;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
        /// 日志
        /// </summary>
        protected readonly ILogger<BaseController> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        public BaseController()
        {
            this._mapper = CoreAppContext.GetService<IMapper>();
            this._logger = CoreAppContext.GetService<ILogger<BaseController>>();
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
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        protected virtual TReturn Invoke<TReturn>(Func<TReturn> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                if (typeof(TReturn) is IActionResult)
                {
                    return (TReturn)JsonFail(ex.Message);
                }
                return default(TReturn);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        protected virtual Task<TReturn> Invoke<TReturn>(Func<Task<TReturn>> action)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                if (typeof(TReturn) is IActionResult)
                {
                    return Task.FromResult((TReturn)JsonFail(ex.Message));
                }
                return Task.FromResult(default(TReturn));
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual IActionResult AddDto<TEntity, TDto, TResult>(TDto dto) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Verification<TEntity, TDto, TResult>(dto, (tEntity, baseService) =>
            {
                var tEntityId = baseService.Add(tEntity);
                return (tEntityId > 0, tEntityId > 0 ? "添加成功!" : "添加失败!");
            });
        }

        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dtos"></param>
        /// <returns></returns>
        protected virtual IActionResult AddDto<TEntity, TDto, TResult>(IList<TDto> dtos) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Verification<TEntity, TDto, TResult>(dtos, (tEntities, baseService) =>
            {
                var tEntityId = baseService.Add(tEntities);
                return (tEntityId > 0, tEntityId > 0 ? "批量添加成功!" : "批量添加失败!");
            });
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected virtual IActionResult UpdateDto<TEntity, TDto, TResult>(TDto dto) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Verification<TEntity, TDto, TResult>(dto, (tEntity, baseService) =>
            {
                var tEntityId = baseService.Update(tEntity);
                return (tEntityId > 0, tEntityId > 0 ? "更新成功!" : "更新失败!");
            });
        }

        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dtos"></param>
        /// <returns></returns>
        protected virtual IActionResult UpdateDto<TEntity, TDto, TResult>(IList<TDto> dtos) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Verification<TEntity, TDto, TResult>(dtos, (tEntities, baseService) =>
            {
                var tEntityId = baseService.Update(tEntities);
                return (tEntityId > 0, tEntityId > 0 ? "批量更新成功!" : "批量更新失败!");
            });
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected virtual IActionResult DeleteDto<TEntity, TDto>(int Id) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto, new()
        {
            TDto dto = new TDto() { Id = Id };
            return DeleteDto<TEntity, TDto>(dto);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        protected virtual IActionResult DeleteDto<TEntity, TDto>(TDto dto) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Verification<TEntity, TDto>(dto, (tEntities, baseService) =>
            {
                var tEntityId = baseService.Update(tEntities);
                return (tEntityId > 0, tEntityId > 0 ? "批量更新成功!" : "批量更新失败!");
            });
        }

        /// <summary>
        /// 获取服务实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expressions"></param>
        /// <returns></returns>
        protected virtual BaseDomainService<int, TEntity> GetInstance<TEntity>() where TEntity : class, IAggregateRoot<int>
        {
            return CoreAppContext.GetService<BaseDomainService<int, TEntity>>();
        }

        /// <summary>
        /// 数据验证（无返回值）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dto"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected virtual IActionResult Verification<TEntity, TDto>(TDto dto, Func<TEntity, BaseDomainService<int, TEntity>, (bool, string)> func) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Invoke<IActionResult>(() =>
            {
                if (!ModelState.IsValid)
                {
                    return JsonFail("参数验证失败!");
                }

                var tEntity = MapTo<TDto, TEntity>(dto);
                if (tEntity != null)
                {
                    var baseService = GetInstance<TEntity>();
                    if (baseService == null)
                    {
                        return JsonFail("服务注入失败!");
                    }
                    var result = func(tEntity, baseService);
                    if (result.Item1)
                    {
                        return JsonSuccess(result.Item2);
                    }
                    else
                    {
                        return JsonFail(result.Item2);
                    }
                }
                return JsonFail("参数映射失败!");
            });
        }

        /// <summary>
        /// 数据验证
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dto"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected virtual IActionResult Verification<TEntity, TDto, TResult>(TDto dto, Func<TEntity, BaseDomainService<int, TEntity>, (bool, string)> func) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Invoke<IActionResult>(() =>
            {
                if (!ModelState.IsValid)
                {
                    return JsonFail("参数验证失败!");
                }

                var tEntity = MapTo<TDto, TEntity>(dto);
                if (tEntity != null)
                {
                    var baseService = GetInstance<TEntity>();
                    if (baseService == null)
                    {
                        return JsonFail("服务注入失败!");
                    }
                    var result = func(tEntity, baseService);
                    if (result.Item1)
                    {
                        return JsonSuccess(result.Item2, MapForm<TEntity, TResult>(tEntity));
                    }
                    else
                    {
                        return JsonFail(result.Item2);
                    }
                }
                return JsonFail("参数映射失败!");
            });
        }

        /// <summary>
        /// 数据验证（批量）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="dto"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected virtual IActionResult Verification<TEntity, TDto, TResult>(IList<TDto> dtos, Func<IList<TEntity>, BaseDomainService<int, TEntity>, (bool, string)> func) where TEntity : class, IAggregateRoot<int> where TDto : BaseDto
        {
            return Invoke<IActionResult>(() =>
            {
                if (!ModelState.IsValid)
                {
                    return JsonFail("参数验证失败!");
                }

                var tEntities = MapTo<IList<TDto>, IList<TEntity>>(dtos);
                if (tEntities != null)
                {
                    var baseService = GetInstance<TEntity>();
                    if (baseService == null)
                    {
                        return JsonFail("服务注入失败!");
                    }
                    var result = func(tEntities, baseService);
                    if (result.Item1)
                    {
                        return JsonSuccess(result.Item2, MapForm<IList<TEntity>, IList<TResult>>(tEntities));
                    }
                    else
                    {
                        return JsonFail(result.Item2);
                    }
                }
                return JsonFail("参数映射失败!");
            });
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual IActionResult JsonSuccess(string message)
        {
            return Json(new CoreResult { Success = true, Message = message });
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual IActionResult JsonSuccess<T>(string message, T value = default(T))
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

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual Task<IActionResult> JsonFailAsync(string message)
        {
            return Task.FromResult<IActionResult>(Json(new CoreResult { Success = false, Message = message }));
        }
    }
}
