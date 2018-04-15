using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class add_userIdcard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "WOrder_Schedule",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdCard",
                table: "WOrder_Account",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "WOrder_Schedule");

            migrationBuilder.DropColumn(
                name: "IdCard",
                table: "WOrder_Account");
        }
    }
}
