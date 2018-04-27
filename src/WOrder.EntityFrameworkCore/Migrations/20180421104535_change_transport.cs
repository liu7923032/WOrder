using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class change_transport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Account_WOrder_Transport_WOrder_TransportId",
                table: "WOrder_Account");

            migrationBuilder.DropTable(
                name: "WOrder_TranUser");

            migrationBuilder.DropIndex(
                name: "IX_WOrder_Account_WOrder_TransportId",
                table: "WOrder_Account");

            migrationBuilder.DropColumn(
                name: "OAddress",
                table: "WOrder_Transport");

            migrationBuilder.DropColumn(
                name: "WOrder_TransportId",
                table: "WOrder_Account");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "WOrder_Transport",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "WOrder_Transport",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WOrder_Transport",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "WOrder_Transport",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EndAddr",
                table: "WOrder_Transport",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "WOrder_Transport",
                maxLength: 1000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StartAddr",
                table: "WOrder_Transport",
                maxLength: 2000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TNo",
                table: "WOrder_Transport",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "TransUserId",
                table: "WOrder_Transport",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Transport_CreatorUserId",
                table: "WOrder_Transport",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Transport_TransUserId",
                table: "WOrder_Transport",
                column: "TransUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Transport_WOrder_Account_CreatorUserId",
                table: "WOrder_Transport",
                column: "CreatorUserId",
                principalTable: "WOrder_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Transport_WOrder_Account_TransUserId",
                table: "WOrder_Transport",
                column: "TransUserId",
                principalTable: "WOrder_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Transport_WOrder_Account_CreatorUserId",
                table: "WOrder_Transport");

            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Transport_WOrder_Account_TransUserId",
                table: "WOrder_Transport");

            migrationBuilder.DropIndex(
                name: "IX_WOrder_Transport_CreatorUserId",
                table: "WOrder_Transport");

            migrationBuilder.DropIndex(
                name: "IX_WOrder_Transport_TransUserId",
                table: "WOrder_Transport");

            migrationBuilder.DropColumn(
                name: "EndAddr",
                table: "WOrder_Transport");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "WOrder_Transport");

            migrationBuilder.DropColumn(
                name: "StartAddr",
                table: "WOrder_Transport");

            migrationBuilder.DropColumn(
                name: "TNo",
                table: "WOrder_Transport");

            migrationBuilder.DropColumn(
                name: "TransUserId",
                table: "WOrder_Transport");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "WOrder_Transport",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDate",
                table: "WOrder_Transport",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WOrder_Transport",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "WOrder_Transport",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "OAddress",
                table: "WOrder_Transport",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WOrder_TransportId",
                table: "WOrder_Account",
                nullable: true);

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
    }
}
