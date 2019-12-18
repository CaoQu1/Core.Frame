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
    public class Query : AggregateRoot<Query, int>
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
        public CoreEnum.FieldValueTypeEnum FieldValueTypeEnum { get; set; }

        /// <summary>
        /// 控件类型为下拉列表,数据源数据库链接实例名称
        /// </summary>
        public string ListDbIns { get; set; }

        /// <summary>
        /// 控件类型为下拉列表，数据源SQL语句，必须返回两个以上字段，第一个为 Text,第二个为Value
        /// </summary>
        public string ListSql { get; set; }

        /// <summary>
        /// 菜单查询关联信息
        /// </summary>
        public virtual ICollection<ControllerQuery> ControllerQueries { get; set; }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Query> builder)
        {
            builder.ToTable("Queries");
            builder.HasData(new Query
            {
                Id = 1,
                ControlType = CoreEnum.ControlType.TextBox,
                CreateTime = DateTime.Now,
                FieldPlaceHolder = "请输入关键字查询",
                IsAllowNull = true,
                LabelTitle = "关键字",
                SortId = 1,
                FieldValueName = "KeyWord",
                //ListDbIns = CoreAppContext.Configuration.GetSection("ConnectionStrings")["Core"],
                //ListSql=
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.String
            });

            builder.HasData(new Query
            {
                Id = 2,
                ControlType = CoreEnum.ControlType.DateTime,
                CreateTime = DateTime.Now,
                // FieldPlaceHolder = "请输入关键字查询",
                IsAllowNull = true,
                LabelTitle = "开始时间",
                SortId = 1,
                FieldValueName = "StartTime",
                //ListDbIns = CoreAppContext.Configuration.GetSection("ConnectionStrings")["Core"],
                //ListSql=
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.DateTime
            });

            builder.HasData(new Query
            {
                Id = 3,
                ControlType = CoreEnum.ControlType.DateTime,
                CreateTime = DateTime.Now,
                // FieldPlaceHolder = "请输入关键字查询",
                IsAllowNull = true,
                LabelTitle = "结束时间",
                SortId = 1,
                FieldValueName = "EndTime",
                //ListDbIns = CoreAppContext.Configuration.GetSection("ConnectionStrings")["Core"],
                //ListSql=
                FieldValueTypeEnum = CoreEnum.FieldValueTypeEnum.DateTime
            });

            base.Configure(builder);
        }
    }
}
