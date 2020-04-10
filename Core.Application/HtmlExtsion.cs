using Core.Application.Dto;
using Core.Domain.Entities;
using Core.Global;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application
{
    /// <summary>
    /// html对象扩展
    /// </summary>
    public static class HtmlExtsion
    {
        /// <summary>
        /// 表单外控件
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlContent FormOutControl(this IHtmlHelper htmlHelper)
        {
            return Render(htmlHelper.ViewData["buttonOutList"] as List<PageControl>);
        }

        /// <summary>
        /// 表单内控件
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static IHtmlContent FormInControl(this IHtmlHelper htmlHelper)
        {
            return Render(htmlHelper.ViewData["buttonList"] as List<PageControl>, CoreEnum.ControlPosition.Inside);
        }

        /// <summary>
        /// 渲染控件
        /// </summary>
        /// <param name="pageActions"></param>
        /// <returns></returns>
        private static IHtmlContent Render(List<PageControl> pageControls, CoreEnum.ControlPosition controlPosition = CoreEnum.ControlPosition.OutSide)
        {
            StringBuilder buttonViewResult = new StringBuilder();
            buttonViewResult.Append(@"<div class=""layui-form-item"">");
            foreach (var item in pageControls)
            {
                switch (item.ControlType)
                {
                    case Global.CoreEnum.ControlType.TextBox:
                        buttonViewResult.Append($@"<div class=""layui-inline"">
    <label class=""layui-form-label"">{item.LabelTitle}</label>
    <div class=""layui-input-inline"">
      <input type = ""text"" name=""{item.FieldValueName}"" placeholder=""{item.FieldPlaceHolder}"" class=""layui-input {item.CssStyle}""></div></div>");
                        break;
                    case Global.CoreEnum.ControlType.DropList:
                        if (!string.IsNullOrEmpty(item.ListDbIns))
                        {
                            var dapperClient = Core.Global.CoreAppContext.GetService<IDapperFactory>().CreateClient("sqlserver");
                            var textValues = dapperClient.Query<BaseKeyValue>(item.ListSql);
                            buttonViewResult.Append($@"<div class=""layui-inline"">
    <label class=""layui-form-label"">{item.LabelTitle}</label>
    <div class=""layui-input-inline"">");
                            buttonViewResult.Append($@"<select name = ""{item.FieldValueName}"" lay-filter=""aihao"">");
                            foreach (var keyValue in textValues)
                            {
                                buttonViewResult.Append($@"<option value = ""{keyValue.Value}"">{keyValue.Value}</option >");
                            }
                            buttonViewResult.Append("</select></div></div>");
                        }
                        break;
                    case Global.CoreEnum.ControlType.DateTime:
                        break;
                    case Global.CoreEnum.ControlType.Button:
                        if (controlPosition == CoreEnum.ControlPosition.Inside)
                        {
                            buttonViewResult.Append($@" <a class=""layui-btn {item.CssStyle} layui-btn-xs"" {(!string.IsNullOrEmpty(item.Event) ? "lay-event='" + item.Event + "'" : "")}>{item.LabelTitle}</a>");
                        }
                        else
                        {
                            buttonViewResult.Append($@"
<div class=""layui-inline"">
      <button class=""layui-btn {item.CssStyle}"" {(!string.IsNullOrEmpty(item.Event) ? "lay-event='" + item.Event + "'" : "")} id=""{item.ControlId}"">{item.LabelTitle}</button>
  </div>");
                        }
                        break;
                    default:
                        break;
                }
            }
            buttonViewResult.Append("</div>");
            return new HtmlString(buttonViewResult.ToString());
        }
    }
}
