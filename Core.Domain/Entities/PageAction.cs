using Core.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 查询模型
    /// </summary>
    public class PageAction : AggregateRoot<PageAction, int>
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string LabelTitle { get; set; }

        /// <summary>
        /// 控件类型
        /// </summary>
        public CoreEnum.ControlType ControlType { get; set; }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool? IsAllowNull { get; set; }

        /// <summary>
        /// 存储值的字段名
        /// </summary>
        public string FieldValueName { get; set; }

        /// <summary>
        /// 控件placeholder提示信息
        /// </summary>
        public string FieldPlaceHolder { get; set; }

        /// <summary>
        /// 存储值的数据类型
        /// </summary>
        public CoreEnum.FieldValueTypeEnum? FieldValueTypeEnum { get; set; }

        /// <summary>
        /// 控件类型为下拉列表,数据源数据库链接实例名称
        /// </summary>
        public string ListDbIns { get; set; }

        /// <summary>
        /// 控件类型为下拉列表，数据源SQL语句，必须返回两个以上字段，第一个为 Text,第二个为Value
        /// </summary>
        public string ListSql { get; set; }

        /// <summary>
        /// 样式
        /// </summary>
        public string CssStyle { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public CoreEnum.ActionType ActionType { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        public CoreEnum.Operation? Type { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<PageAction> builder)
        {
            builder.ToTable("PageActions");
            builder.HasData(new PageAction
            {
                Id = 1,
                ControlType = CoreEnum.ControlType.TextBox,
                CreateTime = DateTime.Now,
                FieldPlaceHolder = "请输入关键字查询",
                IsAllowNull = true,
                LabelTitle = "关键字",
                SortId = 1,
                FieldValueName = "KeyWord",
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.String,
                ActionType = CoreEnum.ActionType.OutSide
            });

            builder.HasData(new PageAction
            {
                Id = 2,
                ControlType = CoreEnum.ControlType.DateTime,
                CreateTime = DateTime.Now,
                IsAllowNull = true,
                LabelTitle = "开始时间",
                SortId = 1,
                FieldValueName = "StartTime",
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.DateTime,
                ActionType = CoreEnum.ActionType.OutSide
            });

            builder.HasData(new PageAction
            {
                Id = 3,
                ControlType = CoreEnum.ControlType.DateTime,
                CreateTime = DateTime.Now,
                IsAllowNull = true,
                LabelTitle = "结束时间",
                SortId = 1,
                FieldValueName = "EndTime",
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.DateTime,
                ActionType = CoreEnum.ActionType.OutSide
            });


            builder.HasData(new PageAction
            {
                Id = 4,
                ControlType = CoreEnum.ControlType.Button,
                CreateTime = DateTime.Now,
                FieldPlaceHolder = "添加",
                LabelTitle = "",
                SortId = 1,
                ActionType = CoreEnum.ActionType.OutSide,
                Type = CoreEnum.Operation.Add,
                CssStyle = "laybtn"
            });


            builder.HasData(new PageAction
            {
                Id = 5,
                ControlType = CoreEnum.ControlType.Button,
                CreateTime = DateTime.Now,
                FieldPlaceHolder = "编辑",
                LabelTitle = "",
                SortId = 1,
                ActionType = CoreEnum.ActionType.Inside,
                Type = CoreEnum.Operation.Update,
                CssStyle = "laybtn"
            });

            base.Configure(builder);
        }
    }
}
