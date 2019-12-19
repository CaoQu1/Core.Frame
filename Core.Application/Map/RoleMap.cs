using AutoMapper;
using Core.Application.Dto.EditDto;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Map
{
    /// <summary>
    /// dto和entity互转
    /// </summary>
    public class RoleMap : Profile
    {
        public RoleMap()
        {
            CreateMap<Role, RoleEditDto>();
            CreateMap<RoleEditDto, Role>();
        }
    }
}
