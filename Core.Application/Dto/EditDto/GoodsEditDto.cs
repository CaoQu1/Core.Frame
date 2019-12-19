using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Application.Dto.EditDto
{
    public class GoodsEditDto : BaseQueryPageDto
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string GoodNo { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string GoodName { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int StockQuantity { get; set; }

        /// <summary>
        /// 所需积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 是否幻灯片
        /// </summary>
        public bool? IsSlide { get; set; }

        /// <summary>
        /// 商品类型
        /// </summary>
        public int GoodCategoryId { get; set; }

        /// <summary>
        /// 缩略图
        /// </summary>
        public string ThumbnailImg { get; set; }

        /// <summary>
        /// 详细内容
        /// </summary>
        public string DetailContent { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortId { get; set; }

    }
}
