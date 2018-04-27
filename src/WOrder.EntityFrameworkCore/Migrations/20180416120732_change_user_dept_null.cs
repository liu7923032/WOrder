using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class change_user_dept_null : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Account_WOrder_Department_DeptId",
                table: "WOrder_Account");

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "WOrder_Account",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Account_WOrder_Department_DeptId",
                table: "WOrder_Account",
                column: "DeptId",
                principalTable: "WOrder_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Account_WOrder_Department_DeptId",
                table: "WOrder_Account");

            migrationBuilder.AlterColumn<int>(
                name: "DeptId",
                table: "WOrder_Account",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Account_WOrder_Department_DeptId",
                table: "WOrder_Account",
                column: "DeptId",
                principalTable: "WOrder_Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
