using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class Add_HandlerLastUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "WOrder_Handler",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "WOrder_Handler",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "WOrder_Account",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WOrder_Account",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "WOrder_Handler");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "WOrder_Handler");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WOrder_Account");

            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "WOrder_Account",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
