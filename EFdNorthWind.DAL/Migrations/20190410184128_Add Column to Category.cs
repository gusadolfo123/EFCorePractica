using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFdNorthWind.DAL.Migrations
{
    public partial class AddColumntoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Categories",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Categories");
        }
    }
}
