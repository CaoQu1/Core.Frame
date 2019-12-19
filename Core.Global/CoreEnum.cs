using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Global
{
    /// <summary>
    /// 系统枚举
    /// </summary>
    public class CoreEnum
    {
        /// <summary>
        /// 排序
        /// </summary>
        public enum SortOrder
        {
            /// <summary>
            ///倒序
            /// </summary>
            Desc,

            /// <summary>
            /// 正序
            /// </summary>
            Asc
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// 男
            /// </summary>
            Man,

            /// <summary>
            /// 女
            /// </summary>
            WoMan
        }

        /// <summary>
        /// 状态
        /// </summary>
        public enum Status
        {
            /// <summary>
            /// 启用
            /// </summary>
            Enable,

            /// <summary>
            ///禁用
            /// </summary>
            Disenable
        }

        /// <summary>
        /// 操作
        /// </summary>
        public enum Operation
        {
            /// <summary>
            /// 增加
            /// </summary>
            Add,

            /// <summary>
            /// 删除
            /// </summary>
            Delete,

            /// <summary>
            /// 查询
            /// </summary>
            Query,

            /// <summary>
            /// 更新
            /// </summary>
            Update,

            /// <summary>
            /// 审核
            /// </summary>
            Approve,
        }

        /// <summary>
        /// 附件类型
        /// </summary>
        public enum AttachmentType
        {
            /// <summary>
            /// 图片
            /// </summary>
            Image,

            /// <summary>
            /// 文件
            /// </summary>
            File
        }

        /// <summary>
        /// 模块
        /// </summary>
        public enum Module
        {
            /// <summary>
            /// 商品
            /// </summary>
            Good,

            /// <summary>
            /// 其他
            /// </summary>
            Other
        }

        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 生成
            /// </summary>
            Create,

            /// <summary>
            /// 确认
            /// </summary>
            Cofirm,

            /// <summary>
            /// 完成
            /// </summary>
            Finish
        }

        /// <summary>
        /// 支付状态
        /// </summary>
        public enum PaymentStatus
        {
            /// <summary>
            /// 已支付
            /// </summary>
            Piad,
            /// <summary>
            /// 未支付
            /// </summary>
            UnPaid
        }

        /// <summary>
        /// 发货状态
        /// </summary>
        public enum DistributionStatus
        {
            /// <summary>
            /// 已发货
            /// </summary>
            Shipped,

            /// <summary>
            /// 未发货
            /// </summary>
            UnShipped
        }

        /// <summary>
        /// 获取红包方式
        /// </summary>
        public enum GetRedPackType
        {
            /// <summary>
            /// 随机
            /// </summary>
            Random,

            /// <summary>
            /// 固定
            /// </summary>
            Fixed
        }

        /// <summary>
        /// 会员类型
        /// </summary>
        public enum UserType
        {
            /// <summary>
            /// 水电工
            /// </summary>
            Plumber,

            /// <summary>
            /// 五金门店
            /// </summary>
            Shop,

            /// <summary>
            /// 分销商
            /// </summary>
            Distributor,

            /// <summary>
            /// 一级代理商
            /// </summary>
            Agent
        }

        /// <summary>
        /// 验证码类型
        /// </summary>
        public enum CodeType
        {
            /// <summary>
            /// 注册
            /// </summary>
            PhoneVerify,

            /// <summary>
            /// 其他
            /// </summary>
            Other
        }

        /// <summary>
        /// 表单操作
        /// </summary>
        public enum ActionType
        {
            /// <summary>
            /// 表单内
            /// </summary>
            Inside,

            /// <summary>
            /// 表单外
            /// </summary>
            OutSide
        }

        /// <summary>
        /// 控件类型
        /// </summary>
        public enum ControlType
        {
            /// <summary>
            /// 输入框
            /// </summary>
            TextBox,

            /// <summary>
            ///下拉框
            /// </summary>
            DropList,

            /// <summary>
            /// 时间选择
            /// </summary>
            DateTime,

            /// <summary>
            /// 按钮
            /// </summary>
            Button
        }

        /// <summary>
        /// 值的数据类型
        /// </summary>
        public enum FieldValueTypeEnum
        {
            /// <summary>
            /// 整型
            /// </summary>
            Int = 1,

            /// <summary>
            /// 浮点型
            /// </summary>
            Double = 2,

            /// <summary>
            /// 时间
            /// </summary>
            DateTime = 3,

            /// <summary>
            /// 字符串
            /// </summary>
            String = 4,

            /// <summary>
            /// guid
            /// </summary>
            GUID = 5
        }
    }
}
