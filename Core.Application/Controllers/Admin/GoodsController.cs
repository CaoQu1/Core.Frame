using AutoMapper;
using Core.Application.Dto;
using Core.Application.Dto.EditDto;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Repositories;
using Core.Domain.Service;
using Core.Global;
using Core.Global.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Application.Controllers.Admin
{
    public class GoodsController : AdminBaseController
    {
        private readonly BaseDomainService<int, Goods> _baseDomainService;

        public GoodsController(BaseDomainService<int, Goods> baseDomainService)
        {
            _baseDomainService = baseDomainService;
        }

        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult GoodsManager()
        {
            return View();
        }

        /// <summary>
        /// 弹出层
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult GoodsEdit()
        {
            return View();
        }
        /// <summary>
        /// 获取商品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetNewsList(GoodsDto goodsFilter)
        {
            var data = new ExpressionSpecification<Goods>(x => true);

            data.And(new ExpressionSpecification<Goods>(x => x.Point == 110));
            data.And(new ExpressionSpecification<Goods>(x => x.GoodName == "123"));
            var goods = GoodsService.Instance.GetByCondition(data); //, x => x.GoodCategory

            //_baseDomainService.Delete()
            GoodsService.Instance.Add(MapTo<GoodsDto, Goods>(goodsFilter));
            //foreach (var item in goods) {
            //    MapForm<Goods, GoodsDto>(item);
            //}

            return null;
        }
        /// <summary>
        /// 新增/修改
        /// </summary>
        /// <returns></returns>
        public ActionResult EditNewsType(Goods goodsModel)
        {
            string msg = "操作成功";

            if (string.IsNullOrEmpty(goodsModel.GoodNo))
            {
                Goods model = new Goods();
                model = goodsModel;
                //var result = NewsTypeServer.Insert(nt);
                //if (result == null)
                //    msg = "失败";
            }
            else
            {
                //var type = NewsTypeServer.Query(x => x.TypeID == id).First();
                //type.TypeName = typeName;
                //if (!NewsTypeServer.Update(type))
                //    msg = "失败";
            }
            //return Json(new { code = 400, msg = msg }, JsonRequestBehavior.AllowGet);
            return null;
        }
        //删除这个商品
        public ActionResult DelGoods(string GoodNo)
        {

            string msg = "操作成功";
            int code = 400;
            var goods = new ExpressionSpecification<Goods>(x => x.GoodNo == GoodNo);

            if (goods != null)
            {//执行删除

            }
            else
            {
                msg = "删除失败";
            }
            // return Json(new { code = code, msg = msg }, JsonRequestBehavior.AllowGet);
            return null;
        }

    }
}
