using AutoMapper;
using Core.Application.Dto;
using Core.Application.Dto.EditDto;
using Core.Application.Dto.ReturnDto;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Map
{
    /// <summary>
    /// dto和entity互转
    /// </summary>
    public class MenuMap : Profile
    {
        public MenuMap()
        {
            this.CreateMap<MenuEditDto, ControllerPermissions>();
            this.CreateMap<ControllerPermissions, MenuReturnDto>();
        }
    }
}
