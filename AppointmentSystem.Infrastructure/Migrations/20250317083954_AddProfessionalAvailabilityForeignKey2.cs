using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionalAvailabilityForeignKey2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProfessionalAvailabilityId",
                table: "Appointments");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_Id",
                table: "Appointments",
                column: "Id",
                principalTable: "ProfessionalAvailabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_Id",
                table: "Appointments");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProfessionalAvailabilityId",
                table: "Appointments",
                column: "ProfessionalAvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
                table: "Appointments",
                column: "ProfessionalAvailabilityId",
                principalTable: "ProfessionalAvailabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
