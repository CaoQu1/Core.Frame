using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Applications
{
    /// <summary>
    /// 应用接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseApplication<TEntity>
    {
        TEntity MapTo<TDto>(TDto dto);

        TDto MapForm<TDto>(TEntity dto);
    }
}
