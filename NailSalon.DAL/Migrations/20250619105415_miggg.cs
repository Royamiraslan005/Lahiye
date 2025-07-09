using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailSalon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class miggg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id");
        }
    }
}
