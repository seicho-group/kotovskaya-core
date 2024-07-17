using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kotovskaya.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddedPathname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PathName",
                table: "Categories",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PathName",
                table: "Categories");
        }
    }
}
