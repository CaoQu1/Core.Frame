using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto.ReturnDto
{
    /// <summary>
    /// 菜单信息
    /// </summary>
    public class MenuReturnDto : BaseDto
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块地址
        /// </summary>
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

        /// <summary>
        /// 打开
        /// </summary>
        public bool Spread { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuReturnDto> Children { get; set; }
    }
}
