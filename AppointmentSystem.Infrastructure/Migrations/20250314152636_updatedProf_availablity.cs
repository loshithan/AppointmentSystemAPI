using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppointmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedProf_availablity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HospitalAddress",
                table: "ProfessionalAvailabilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HospitalName",
                table: "ProfessionalAvailabilities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Specialisation",
                table: "ProfessionalAvailabilities",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HospitalAddress",
                table: "ProfessionalAvailabilities");

            migrationBuilder.DropColumn(
                name: "HospitalName",
                table: "ProfessionalAvailabilities");

            migrationBuilder.DropColumn(
                name: "Specialisation",
                table: "ProfessionalAvailabilities");
        }
    }
}
