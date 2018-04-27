using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class change_message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Sys_Message",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppPage",
                table: "Sys_Message",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SrcId",
                table: "Sys_Message",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Message_UserId",
                table: "Sys_Message",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sys_Message_WOrder_Account_UserId",
                table: "Sys_Message",
                column: "UserId",
                principalTable: "WOrder_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sys_Message_WOrder_Account_UserId",
                table: "Sys_Message");

            migrationBuilder.DropIndex(
                name: "IX_Sys_Message_UserId",
                table: "Sys_Message");

            migrationBuilder.DropColumn(
                name: "AppPage",
                table: "Sys_Message");

            migrationBuilder.DropColumn(
                name: "SrcId",
                table: "Sys_Message");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Sys_Message",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);
        }
    }
}
