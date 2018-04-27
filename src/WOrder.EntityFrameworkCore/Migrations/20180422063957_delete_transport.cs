using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class delete_transport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WOrder_OrderRecord");

            migrationBuilder.DropTable(
                name: "WOrder_Transport");

            migrationBuilder.AddColumn<long>(
                name: "HandlerId",
                table: "WOrder_Order",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
             name: "SDate",
             table: "WOrder_Order",
             nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                 name: "EDate",
                 table: "WOrder_Order",
                 nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartAddr",
                table: "WOrder_Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Order_HandlerId",
                table: "WOrder_Order",
                column: "HandlerId");

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Order_WOrder_Account_HandlerId",
                table: "WOrder_Order",
                column: "HandlerId",
                principalTable: "WOrder_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Order_WOrder_Account_HandlerId",
                table: "WOrder_Order");

            migrationBuilder.DropIndex(
                name: "IX_WOrder_Order_HandlerId",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "HandlerId",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "StartAddr",
                table: "WOrder_Order");

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

            migrationBuilder.CreateTable(
                name: "WOrder_Transport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcceptUID = table.Column<int>(nullable: true),
                    AcceptUserId = table.Column<long>(nullable: true),
                    Category = table.Column<string>(maxLength: 100, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    EDate = table.Column<DateTime>(nullable: false),
                    EndAddr = table.Column<string>(maxLength: 100, nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 1000, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Location = table.Column<string>(maxLength: 200, nullable: true),
                    SDate = table.Column<DateTime>(nullable: false),
                    StartAddr = table.Column<string>(maxLength: 2000, nullable: false),
                    TNo = table.Column<string>(maxLength: 50, nullable: false),
                    TStatus = table.Column<int>(nullable: false),
                    TransUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Transport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_Transport_WOrder_Account_AcceptUserId",
                        column: x => x.AcceptUserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WOrder_Transport_WOrder_Account_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WOrder_Transport_WOrder_Account_TransUserId",
                        column: x => x.TransUserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_OrderRecord_HandlerId",
                table: "WOrder_OrderRecord",
                column: "HandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Transport_AcceptUserId",
                table: "WOrder_Transport",
                column: "AcceptUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Transport_CreatorUserId",
                table: "WOrder_Transport",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Transport_TransUserId",
                table: "WOrder_Transport",
                column: "TransUserId");
        }
    }
}
