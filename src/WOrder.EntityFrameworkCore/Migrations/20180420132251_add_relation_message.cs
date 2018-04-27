using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WOrder.Migrations
{
    public partial class add_relation_message : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemType",
                table: "WOrder_Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "WOrder_Order",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "WOrder_Order",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "WOrder_Order",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "WOrder_Account",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "WOrder_Account",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "WOrder_Account",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Sys_Message",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    Target = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sys_Message_WOrder_Account_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "WOrder_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WOrder_Relation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditedId = table.Column<long>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    OrderId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WOrder_Relation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sys_Message_CreatorUserId",
                table: "Sys_Message",
                column: "CreatorUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_Message");

            migrationBuilder.DropTable(
                name: "WOrder_Relation");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "WOrder_Order");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "WOrder_Account");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "WOrder_Account");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "WOrder_Account");

            migrationBuilder.AddColumn<string>(
                name: "ItemType",
                table: "WOrder_Order",
                maxLength: 100,
                nullable: true);
        }
    }
}
