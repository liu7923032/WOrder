﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WOrder.Domain.Entities;
using WOrder.EntityFrameworkCore;

namespace WOrder.Migrations
{
    [DbContext(typeof(WOrderDbContext))]
    [Migration("20180317065503_add_schedule")]
    partial class add_schedule
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Account", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<string>("AreaName")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("DeptId");

                    b.Property<string>("Email")
                        .HasMaxLength(40);

                    b.Property<decimal?>("Integral");

                    b.Property<bool>("IsLock");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Password")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .HasMaxLength(40);

                    b.Property<string>("Photos")
                        .HasMaxLength(100);

                    b.Property<string>("Position")
                        .HasMaxLength(50);

                    b.Property<string>("Sex")
                        .HasMaxLength(10);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("WorkMode");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.ToTable("WOrder_Account");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_AttachFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType")
                        .IsRequired();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("Describe");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("FilePath")
                        .IsRequired();

                    b.Property<string>("FileSize");

                    b.Property<string>("FileType")
                        .IsRequired();

                    b.Property<string>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("WOrder_AttachFile");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CStatus");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("WOrder_Comment");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DeptNo")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("InputCode")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Position")
                        .HasMaxLength(300);

                    b.HasKey("Id");

                    b.ToTable("WOrder_Department");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Dictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("DictType")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Memo")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("No")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SortNo");

                    b.HasKey("Id");

                    b.ToTable("WOrder_Dictionary");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_DictType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("Memo")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("SortNo");

                    b.HasKey("Id");

                    b.ToTable("WOrder_DictType");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Handler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long>("HandleId");

                    b.Property<int>("OStatus");

                    b.Property<int>("OrderId");

                    b.HasKey("Id");

                    b.HasIndex("HandleId");

                    b.HasIndex("OrderId");

                    b.ToTable("WOrder_Handler");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Integral", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("ActDate");

                    b.Property<int>("CostType");

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<decimal>("Current");

                    b.Property<int>("DeptId");

                    b.Property<string>("Describe");

                    b.Property<decimal>("Integral");

                    b.Property<string>("TypeName")
                        .IsRequired();

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WOrder_Integral");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<long?>("DeleterUserId");

                    b.Property<DateTime?>("DeletionTime");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("OAddress")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("OrderNo")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.Property<int>("OrderType");

                    b.Property<int>("TStatus");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("LastModifierUserId");

                    b.ToTable("WOrder_Order");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_ORecord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<int>("HandlerId");

                    b.Property<int>("OStatus");

                    b.HasKey("Id");

                    b.HasIndex("HandlerId");

                    b.ToTable("WOrder_OrderRecord");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Account", b =>
                {
                    b.HasOne("WOrder.Domain.Entities.WOrder_Department", "Department")
                        .WithMany("Accounts")
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Comment", b =>
                {
                    b.HasOne("WOrder.Domain.Entities.WOrder_Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Handler", b =>
                {
                    b.HasOne("WOrder.Domain.Entities.WOrder_Account", "Handler")
                        .WithMany()
                        .HasForeignKey("HandleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WOrder.Domain.Entities.WOrder_Order", "Order")
                        .WithMany("Handlers")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Integral", b =>
                {
                    b.HasOne("WOrder.Domain.Entities.WOrder_Account", "Account")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_Order", b =>
                {
                    b.HasOne("WOrder.Domain.Entities.WOrder_Account", "CreatorUser")
                        .WithMany()
                        .HasForeignKey("CreatorUserId");

                    b.HasOne("WOrder.Domain.Entities.WOrder_Account", "LastModifierUser")
                        .WithMany()
                        .HasForeignKey("LastModifierUserId");
                });

            modelBuilder.Entity("WOrder.Domain.Entities.WOrder_ORecord", b =>
                {
                    b.HasOne("WOrder.Domain.Entities.WOrder_Handler", "Handler")
                        .WithMany("Records")
                        .HasForeignKey("HandlerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
