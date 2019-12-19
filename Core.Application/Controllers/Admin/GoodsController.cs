using AutoMapper;
using Core.Application.Dto;
using Core.Application.Dto.EditDto;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Core.Global.Attributes;
using Core.Global.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Controllers.Admin
{
    /// <summary>
    /// 商品管理控制器
    /// </summary>
    [Initialize("商品管理", Area = "Admin", ModuleUrl = "/Admin/Goods/Index")]
    public class GoodsController : AdminBaseController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseDomainService"></param>
        public GoodsController()
        {
        }

        /// <summary>
        /// 商品列表页
        /// </summary>
        /// <returns></returns>
        [Initialize("商品列表页")]
        public override IActionResult Index()
        {
            return base.Index();
        }

        /// <summary>
        /// 编辑商品页
        /// </summary>
        /// <returns></returns>
        [Initialize("编辑商品页")]
        public IActionResult EditGoods()
        {
            return View();
        }

        /// <summary>
        /// 保存商品
        /// </summary>
        /// <returns></returns>
        [Initialize("保存商品")]
        public IActionResult EditGoods(GoodsEditDto goodsEditDto)
        {
            return Edit<Goods, GoodsEditDto, Goods>(goodsEditDto);
        }

        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        [Initialize("获取商品分页数据")]
        public async Task<IActionResult> GetGoodsListAsync(BaseQueryPageDto baseQueryPageDto)
        {
            return await GetPageListAsync<Goods, BaseQueryPageDto, Goods>(baseQueryPageDto);
        }

        /// <summary>
        /// 删除商品
        /// </summary>
        /// <param name="GoodNo"></param>
        /// <returns></returns>
        [Initialize("删除商品")]
        public IActionResult DelGoods(int id)
        {
            return DeleteDto<Goods>(id);
        }
    }
}
