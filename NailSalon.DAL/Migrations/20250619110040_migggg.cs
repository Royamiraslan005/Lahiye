using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NailSalon.DAL.Migrations
{
    /// <inheritdoc />
    public partial class migggg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "DesignId",
                table: "Reservations",
                newName: "NailTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_DesignId",
                table: "Reservations",
                newName: "IX_Reservations_NailTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_NailTypes_NailTypeId",
                table: "Reservations",
                column: "NailTypeId",
                principalTable: "NailTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_NailTypes_NailTypeId",
                table: "Reservations");

            migrationBuilder.RenameColumn(
                name: "NailTypeId",
                table: "Reservations",
                newName: "DesignId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_NailTypeId",
                table: "Reservations",
                newName: "IX_Reservations_DesignId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Designs_DesignId",
                table: "Reservations",
                column: "DesignId",
                principalTable: "Designs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
