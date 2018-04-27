using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class add_transport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArriveDate",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WOrder_Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptDate",
                table: "WOrder_Handler",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "WOrder_Handler",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WOrder_TransportId",
                table: "WOrder_Account",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WOrder_Transport",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcceptUID = table.Column<int>(nullable: true),
                    AcceptUserId = table.Column<long>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    OAddress = table.Column<string>(nullable: true),
                    SDate = table.Column<DateTime>(nullable: false),
                    TStatus = table.Column<int>(nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "WOrder_TranUser",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AcceptDate = table.Column<DateTime>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TransId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_TranUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WOrder_TranUser_WOrder_Transport_TransId",
                        column: x => x.TransId,
                        principalTable: "WOrder_Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WOrder_TranUser_WOrder_Account_UserId",
                        column: x => x.UserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Account_WOrder_TransportId",
                table: "WOrder_Account",
                column: "WOrder_TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Transport_AcceptUserId",
                table: "WOrder_Transport",
                column: "AcceptUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_TranUser_TransId",
                table: "WOrder_TranUser",
                column: "TransId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_TranUser_UserId",
                table: "WOrder_TranUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Account_WOrder_Transport_WOrder_TransportId",
                table: "WOrder_Account",
                column: "WOrder_TransportId",
                principalTable: "WOrder_Transport",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Account_WOrder_Transport_WOrder_TransportId",
                table: "WOrder_Account");

            migrationBuilder.DropTable(
                name: "WOrder_TranUser");

            migrationBuilder.DropTable(
                name: "WOrder_Transport");

            migrationBuilder.DropIndex(
                name: "IX_WOrder_Account_WOrder_TransportId",
                table: "WOrder_Account");

            migrationBuilder.DropColumn(
                name: "AcceptDate",
                table: "WOrder_Handler");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "WOrder_Handler");

            migrationBuilder.DropColumn(
                name: "WOrder_TransportId",
                table: "WOrder_Account");

            migrationBuilder.AddColumn<DateTime>(
                name: "ArriveDate",
                table: "WOrder_Order",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WOrder_Order",
                nullable: true);
        }
    }
}
