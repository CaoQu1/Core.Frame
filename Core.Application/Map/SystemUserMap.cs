using AutoMapper;
using Core.Application.Dto;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Map
{
    /// <summary>
    /// dto和entity互转
    /// </summary>
    public class SystemUserMap : Profile
    {
        public SystemUserMap()
        {
            this.CreateMap<SystemUserDto, SystemUser>();
            this.CreateMap<SystemUser, SystemUserDto>();
        }
    }
}
