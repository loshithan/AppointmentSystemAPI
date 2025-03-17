using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionalAvailabilityForeignKey3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_ProfessionalId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_Id",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProfessionalId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessionalId",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProfessionalAvailabilityId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessionalId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ProfessionalId",
                table: "Appointments",
                column: "ProfessionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_AspNetUsers_ProfessionalId",
                table: "Appointments",
                column: "ProfessionalId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_Id",
                table: "Appointments",
                column: "Id",
                principalTable: "ProfessionalAvailabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
