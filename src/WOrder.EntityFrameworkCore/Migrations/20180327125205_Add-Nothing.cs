using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class AddNothing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "WOrder_Schedule");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "WOrder_Schedule");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WOrder_Schedule");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "WOrder_Schedule");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "WOrder_Schedule");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "WOrder_Schedule",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "WOrder_Schedule",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WOrder_Schedule",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "WOrder_Schedule",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "WOrder_Schedule",
                nullable: true);
        }
    }
}
