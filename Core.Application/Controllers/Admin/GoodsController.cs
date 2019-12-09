﻿using AutoMapper;
using Core.Application.Dto;
using Core.Domain;
using Core.Domain.Entities;
using Core.Domain.Repositories;
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
        public ActionResult GetNewsList(Goods goodsFilter, PageInfo pageInfo)
        {
            var data = new ExpressionSpecification<Goods>(x => true);
            //if (!string.IsNullOrEmpty(goodsFilter.GoodName))
            //{
            //    data = data.GetExpression<Goods>(x=>x.)
            //    //Where(x => x.Title.Contains(NewsName));
            //}
            //if (newsType != null)
            //{
            //    data = data.Where(x => x.NewsType == newsType);
            //}
            //int cnt = 0;
            //var ar = data.OrderBy(m => m.Sort).ThenByDescending(x => x.CreateOn).Skip((pageInfo.page - 1) * pageInfo.limit).Take(pageInfo.limit).ToList();
            ////投影
            //var list = from a in ar
            //           join b in t_userServer.Query(x => true) on a.CreateBy equals b.Id
            //           join c in NewsTypeServer.Query(x => true) on a.NewsType equals c.TypeID
            //           select (new
            //           {
            //               NewsID = a.NewsID,
            //               Title = a.Title,
            //               NewsType = c.TypeName,
            //               CreateBy = b.RealName,
            //               Sort = a.Sort,
            //               IsTop = a.IsTop == 1 ? "是" : "否",
            //               CreateOn = a.CreateOn

            //           });
            //cnt = data.Count();
            //var checks = list.OrderBy(x => x.IsTop).OrderByDescending(x => x.CreateOn).ToList();
            //return Json(new { code = 0, msg = "", count = cnt, data = checks }, JsonRequestBehavior.AllowGet);
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
            var goods = new ExpressionSpecification<Goods>(x => x.GoodNo== GoodNo);

            if (goods !=null)
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
