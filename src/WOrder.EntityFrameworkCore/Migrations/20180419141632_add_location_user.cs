using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class add_location_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WOrder_Location_UserId",
                table: "WOrder_Location",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WOrder_Location_WOrder_Account_UserId",
                table: "WOrder_Location",
                column: "UserId",
                principalTable: "WOrder_Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WOrder_Location_WOrder_Account_UserId",
                table: "WOrder_Location");

            migrationBuilder.DropIndex(
                name: "IX_WOrder_Location_UserId",
                table: "WOrder_Location");
        }
    }
}
