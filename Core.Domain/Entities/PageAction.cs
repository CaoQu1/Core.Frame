using Core.Global;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 控件模型
    /// </summary>
    public class PageControl : AggregateRoot<PageControl, int>
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
        public CoreEnum.ControlPosition ControlPosition { get; set; }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public string ButtonType { get; set; }

        /// <summary>
        /// 控件编号
        /// </summary>
        public string ControlId { get; set; }

        /// <summary>
        /// 事件名称
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<PageControl> builder)
        {
            builder.ToTable("PageControls");
            builder.HasData(new PageControl
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
                ControlPosition = CoreEnum.ControlPosition.OutSide
            });

            builder.HasData(new PageControl
            {
                Id = 2,
                ControlType = CoreEnum.ControlType.DateTime,
                CreateTime = DateTime.Now,
                IsAllowNull = true,
                LabelTitle = "开始时间",
                SortId = 1,
                FieldValueName = "StartTime",
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.DateTime,
                ControlPosition = CoreEnum.ControlPosition.OutSide
            });

            builder.HasData(new PageControl
            {
                Id = 3,
                ControlType = CoreEnum.ControlType.DateTime,
                CreateTime = DateTime.Now,
                IsAllowNull = true,
                LabelTitle = "结束时间",
                SortId = 1,
                FieldValueName = "EndTime",
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.DateTime,
                ControlPosition = CoreEnum.ControlPosition.OutSide
            });


            builder.HasData(new PageControl
            {
                Id = 4,
                ControlType = CoreEnum.ControlType.Button,
                CreateTime = DateTime.Now,
                LabelTitle = "添加",
                SortId = 1,
                ControlPosition = CoreEnum.ControlPosition.OutSide,
                CssStyle = "laybtn",
                ControlId = "btnAdd"
            });


            builder.HasData(new PageControl
            {
                Id = 7,
                ControlType = CoreEnum.ControlType.Button,
                CreateTime = DateTime.Now,
                LabelTitle = "查询",
                SortId = 1,
                ControlPosition = CoreEnum.ControlPosition.OutSide,
                CssStyle = "laybtn",
                ControlId = "btnSearch"
            });

            builder.HasData(new PageControl
            {
                Id = 5,
                ControlType = CoreEnum.ControlType.Button,
                CreateTime = DateTime.Now,
                LabelTitle = "编辑",
                SortId = 1,
                ControlPosition = CoreEnum.ControlPosition.Inside,
                CssStyle = "laybtn",
                Event = "edit"
            });

            builder.HasData(new PageControl
            {
                Id = 6,
                ControlType = CoreEnum.ControlType.Button,
                CreateTime = DateTime.Now,
                LabelTitle = "删除",
                SortId = 1,
                ControlPosition = CoreEnum.ControlPosition.Inside,
                CssStyle = "laybtn",
                Event = "del"
            });

            base.Configure(builder);
        }
    }
}
