using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DragonBugs2020.Migrations
{
    public partial class Initial_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Updated",
                table: "TicketComments",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "TicketComments");
        }
    }
}
