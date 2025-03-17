using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionalAvailabilityForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfessionalAvailabilityId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ProfessionalAvailabilityId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "ProfessionalAvailabilityId",
                table: "Appointments");
        }
    }
}
