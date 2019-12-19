using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto.EditDto
{
    /// <summary>
    /// 编辑角色模型
    /// </summary>
    public class RoleEditDto : BaseDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleCode { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDesc { get; set; }

        /// <summary>
        /// 是否是超级管理员
        /// </summary>
        public bool IsSystemAdmin { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Core.Global.CoreEnum.Status Status { get; set; }
    }
}
