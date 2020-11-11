using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DragonBugs2020.Data.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Tickets",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Tickets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Tickets");
        }
    }
}
