using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WOrder_AttachFile",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentType = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Describe = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false),
                    FileSize = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: false),
                    ParentId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_AttachFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DeptNo = table.Column<string>(maxLength: 200, nullable: false),
                    InputCode = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Position = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Dictionary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    DictType = table.Column<string>(maxLength: 100, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Memo = table.Column<string>(maxLength: 2000, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    No = table.Column<string>(maxLength: 50, nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Dictionary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_DictType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Memo = table.Column<string>(maxLength: 1000, nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_DictType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Account",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(maxLength: 20, nullable: false),
                    AreaName = table.Column<string>(maxLength: 1000, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeptId = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 40, nullable: true),
                    Integral = table.Column<decimal>(nullable: true),
                    IsLock = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Password = table.Column<string>(maxLength: 100, nullable: true),
                    Phone = table.Column<string>(maxLength: 40, nullable: true),
                    Photos = table.Column<string>(maxLength: 100, nullable: true),
                    Position = table.Column<string>(maxLength: 50, nullable: true),
                    Sex = table.Column<string>(maxLength: 10, nullable: true),
                    UserName = table.Column<string>(maxLength: 10, nullable: false),
                    WorkMode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Account", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_Account_WOrder_Department_DeptId",
                        column: x => x.DeptId,
                        principalTable: "WOrder_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Integral",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActDate = table.Column<DateTime>(nullable: false),
                    CostType = table.Column<int>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    Current = table.Column<decimal>(nullable: false),
                    DeptId = table.Column<int>(nullable: false),
                    Describe = table.Column<string>(nullable: true),
                    Integral = table.Column<decimal>(nullable: false),
                    TypeName = table.Column<string>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Integral", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_Integral_WOrder_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Order",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArriveDate = table.Column<DateTime>(nullable: true),
                    Category = table.Column<string>(maxLength: 20, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 200, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    OAddress = table.Column<string>(maxLength: 100, nullable: false),
                    OrderNo = table.Column<string>(maxLength: 20, nullable: false),
                    OrderType = table.Column<int>(nullable: false),
                    TStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_Order_WOrder_Account_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WOrder_Order_WOrder_Account_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Schedule",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClassDate = table.Column<DateTime>(nullable: false),
                    ClassType = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DFlag = table.Column<int>(nullable: false),
                    MFlag = table.Column<int>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    YFlag = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_Schedule_WOrder_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Comment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CStatus = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 500, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    OrderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_Comment_WOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "WOrder_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Handler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    HandleId = table.Column<long>(nullable: false),
                    OStatus = table.Column<int>(nullable: false),
                    OrderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Handler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_Handler_WOrder_Account_HandleId",
                        column: x => x.HandleId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WOrder_Handler_WOrder_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "WOrder_Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_OrderRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    HandlerId = table.Column<int>(nullable: false),
                    OStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_OrderRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_OrderRecord_WOrder_Handler_HandlerId",
                        column: x => x.HandlerId,
                        principalTable: "WOrder_Handler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Account_DeptId",
                table: "WOrder_Account",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Comment_OrderId",
                table: "WOrder_Comment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Handler_HandleId",
                table: "WOrder_Handler",
                column: "HandleId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Handler_OrderId",
                table: "WOrder_Handler",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Integral_UserId",
                table: "WOrder_Integral",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Order_CreatorUserId",
                table: "WOrder_Order",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Order_LastModifierUserId",
                table: "WOrder_Order",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_OrderRecord_HandlerId",
                table: "WOrder_OrderRecord",
                column: "HandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Schedule_UserId",
                table: "WOrder_Schedule",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WOrder_AttachFile");

            migrationBuilder.DropTable(
                name: "WOrder_Comment");

            migrationBuilder.DropTable(
                name: "WOrder_Dictionary");

            migrationBuilder.DropTable(
                name: "WOrder_DictType");

            migrationBuilder.DropTable(
                name: "WOrder_Integral");

            migrationBuilder.DropTable(
                name: "WOrder_OrderRecord");

            migrationBuilder.DropTable(
                name: "WOrder_Schedule");

            migrationBuilder.DropTable(
                name: "WOrder_Handler");

            migrationBuilder.DropTable(
                name: "WOrder_Order");

            migrationBuilder.DropTable(
                name: "WOrder_Account");

            migrationBuilder.DropTable(
                name: "WOrder_Department");
        }
    }
}
