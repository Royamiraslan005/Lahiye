using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailSalon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class migprofile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Reservations_DesignId",
                table: "Reservations",
                column: "DesignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_DesignId",
                table: "Reservations");
        }
    }
}
