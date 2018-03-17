using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class Alter_Order_Desc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Order_WOrder_Account_HandleUId",
                table: "WOrder_Order");

            migrationBuilder.DropIndex(
                name: "IX_WOrder_Order_HandleUId",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "HandleUId",
                table: "WOrder_Order");

            migrationBuilder.RenameColumn(
                name: "Desciption",
                table: "WOrder_Order",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "WOrder_Order",
                newName: "Desciption");

            migrationBuilder.AddColumn<long>(
                name: "HandleUId",
                table: "WOrder_Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Order_HandleUId",
                table: "WOrder_Order",
                column: "HandleUId");

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Order_WOrder_Account_HandleUId",
                table: "WOrder_Order",
                column: "HandleUId",
                principalTable: "WOrder_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
