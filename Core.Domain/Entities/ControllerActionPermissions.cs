using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    /// <summary>
    /// 控制器操作实体
    /// </summary>
    public class ControllerActionPermissions : AggregateRoot<ControllerActionPermissions, int>
    {
        /// <summary>
        /// 操作编号
        /// </summary>
        public int ActionId { get; set; }

        /// <summary>
        /// 系统编号
        /// </summary>
        public int SystemId { get; set; }

        /// <summary>
        /// 控制器编号
        /// </summary>
        public int ControllerId { get; set; }

        /// <summary>
        /// 控制器实体
        /// </summary>
        public virtual ControllerPermissions ControllerPermissions { get; set; }

        /// <summary>
        /// 操作实体
        /// </summary>
        public virtual ActionPermissions ActionPermissions { get; set; }

        /// <summary>
        /// 控制器操作角色关联信息
        /// </summary>
        public virtual ICollection<ContollerActionRole> ContollerActionRoles { get; set; }
    }
}
