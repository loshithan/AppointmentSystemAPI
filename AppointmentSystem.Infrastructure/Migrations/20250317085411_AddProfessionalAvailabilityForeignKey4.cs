using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfessionalAvailabilityForeignKey4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
                table: "Appointments");

            migrationBuilder.AlterColumn<string>(
                name: "ProfessionalId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionalAvailabilityId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
                table: "Appointments",
                column: "ProfessionalAvailabilityId",
                principalTable: "ProfessionalAvailabilities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_AspNetUsers_ProfessionalId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
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

            migrationBuilder.AlterColumn<Guid>(
                name: "ProfessionalAvailabilityId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_ProfessionalAvailabilities_ProfessionalAvailabilityId",
                table: "Appointments",
                column: "ProfessionalAvailabilityId",
                principalTable: "ProfessionalAvailabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
