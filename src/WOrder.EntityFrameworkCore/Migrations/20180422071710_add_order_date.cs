using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class add_order_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<DateTime>(
            //    name: "EDate",
            //    table: "WOrder_Order",
            //    nullable: true);

            //migrationBuilder.AddColumn<DateTime>(
            //    name: "SDate",
            //    table: "WOrder_Order",
            //    nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EDate",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "SDate",
                table: "WOrder_Order");
        }
    }
}
