using AutoMapper;
using Core.Application.Dto;
using Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Map
{
    public class GoodsMap : Profile
    {
        public GoodsMap()
        {
            this.CreateMap<GoodsDto, Goods>();
            this.CreateMap<Goods, GoodsDto>();
        }
    }
}
