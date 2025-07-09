using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailSalon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class sale2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_AppUserId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Sales",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Sales",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_AppUserId",
                table: "Sales",
                newName: "IX_Sales_UserId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Sales",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_AspNetUsers_UserId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sales",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Sales",
                newName: "SaleDate");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_UserId",
                table: "Sales",
                newName: "IX_Sales_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_AspNetUsers_AppUserId",
                table: "Sales",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
