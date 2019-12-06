using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Core.Global.CoreEnum;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 系统附件实体
    /// </summary>
    [Table("SystemAttachments")]
    public class SystemAttachment : AggregateRoot<SystemAttachment, int>
    {
        /// <summary>
        /// 附件类型
        /// </summary>
        public AttachmentType Type { get; set; }

        /// <summary>
        /// 来源编号
        /// </summary>
        public string SourceId { get; set; }

        /// <summary>
        /// 来源模块
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }

        /// <summary>
        /// 商品附件信息
        /// </summary>
        public virtual ICollection<GoodAttachment> GoodAttachments { get; set; }
    }
}
