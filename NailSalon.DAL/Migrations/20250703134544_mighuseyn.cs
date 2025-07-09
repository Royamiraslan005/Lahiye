using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailSalon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mighuseyn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_NailTypes_NailTypeId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_NailTypes_NailTypeId",
                table: "Reservations",
                column: "NailTypeId",
                principalTable: "NailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_NailTypes_NailTypeId",
                table: "Reservations");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_NailTypes_NailTypeId",
                table: "Reservations",
                column: "NailTypeId",
                principalTable: "NailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
