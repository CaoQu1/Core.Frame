using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Application.Dto.EditDto
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    public class MenuEditDto : BaseDto
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "模块名称不能为空!")]
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块地址
        /// </summary>
        [Required(ErrorMessage = "模块地址不能为空!")]
        public string ModuleUrl { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int? ShowOrder { get; set; }

        /// <summary>
        /// 父模块
        /// </summary>
        public int? ParentId { get; set; }
    }
}
