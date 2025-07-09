using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailSalon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Menu");

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Masters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Specialty",
                table: "Masters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zodiac",
                table: "Masters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "Specialty",
                table: "Masters");

            migrationBuilder.DropColumn(
                name: "Zodiac",
                table: "Masters");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Menu",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
