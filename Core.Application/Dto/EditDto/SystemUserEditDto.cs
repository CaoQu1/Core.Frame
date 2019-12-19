using Core.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto.EditDto
{
    public class SystemUserEditDto : BaseDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public CoreEnum.Sex Sex { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string HeadPortrait { get; set; }

        /// <summary>
        /// 注册地址
        /// </summary>
        public string Address { get; set; }
    }
}
