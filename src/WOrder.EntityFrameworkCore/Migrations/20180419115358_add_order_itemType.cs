using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class add_order_itemType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "WOrder_Order",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "WOrder_Order");
        }
    }
}
