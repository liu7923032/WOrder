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
        /// 订单处理人
        /// </summary>
        public virtual DbSet<WOrder_Handler> WOrder_Handler { get; set; }

       
        /// <summary>
        /// 产品分类
        /// </summary>
        public virtual DbSet<WOrder_DictType> WOrder_DictType { get; set; }

        /// <summary>
        /// 商品信息
        /// </summary>
        public virtual DbSet<WOrder_Dictionary> WOrder_Dictionary { get; set; }


        public virtual DbSet<WOrder_Department> WOrder_Department { get; set; }
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

        /// <summary>
        /// 排班信息
        /// </summary>
        public virtual DbSet<WOrder_Schedule> WOrder_Schedule { get; set; }

        /// <summary>
        /// 位置信息
        /// </summary>
        public virtual DbSet<WOrder_Location> WOrder_Location { get; set; }

        public virtual DbSet<Sys_Role> Sys_Role { get; set; }

        public virtual DbSet<Sys_UserRole> Sys_UserRole { get; set; }
        /// <summary>
        /// 系统消息
        /// </summary>
        public virtual DbSet<Sys_Message> Sys_Message { get; set; }

        /// <summary>
        /// 用于关联
        /// </summary>
        public virtual DbSet<WOrder_Relation> WOrder_Relation { get; set; }




        #endregion



        public WOrderDbContext(DbContextOptions<WOrderDbContext> options) 
            : base(options)
        {
            
        }
    }
}
