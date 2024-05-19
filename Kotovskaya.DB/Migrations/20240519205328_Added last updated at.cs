using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kotovskaya.DB.Migrations
{
    /// <inheritdoc />
    public partial class Addedlastupdatedat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedAt",
                table: "Products",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedAt",
                table: "Products");
        }
    }
}
