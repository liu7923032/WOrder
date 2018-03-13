using Abp.EntityFrameworkCore;
using WOrder.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace WOrder.EntityFrameworkCore
{
    public class WOrderDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...
        #region 1.0  Add DbSet properties for your entities...
        
        /// <summary>
        /// 工单
        /// </summary>
        public virtual DbSet<WOrder_Order> WOrder_Order { get; set; }

        /// <summary>
        /// 订单记录变更表
        /// </summary>
        public virtual DbSet<WOrder_OrderRecord> WOrder_OrderRecord { get; set; }

        /// <summary>
        /// 产品分类
        /// </summary>
        public virtual DbSet<WOrder_DictType> WOrder_DictType { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public virtual DbSet<WOrder_Dictionary> WOrder_Dictionary { get; set; }

        /// <summary>
        /// 订单账号
        /// </summary>
        public virtual DbSet<WOrder_Account> WOrder_Account { get; set; }

        /// <summary>
        /// 积分信息
        /// </summary>
        public virtual DbSet<WOrder_Integral> WOrder_Integral { get; set; }

        /// <summary>
        /// 图片附档信息
        /// </summary>
        public virtual DbSet<WOrder_AttachFile> WOrder_AttachFile { get; set; }

        /// <summary>
        /// 评论信息
        /// </summary>
        public virtual DbSet<WOrder_Comment> WOrder_Comment { get; set; }


        #endregion

        public WOrderDbContext(DbContextOptions<WOrderDbContext> options) 
            : base(options)
        {
        }
    }
}
