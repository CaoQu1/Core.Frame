﻿using AutoMapper;
using Core.Application.Dto;
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
    public class SystemUserMap : Profile
    {
        public SystemUserMap()
        {
            this.CreateMap<SystemUserEditDto, SystemUser>();
            this.CreateMap<SystemUser, SystemUserEditDto>();
        }
    }
}
