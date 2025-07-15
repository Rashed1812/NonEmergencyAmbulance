using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class PatientConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PatientConfirmed",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatientConfirmed",
                table: "Requests");
        }
    }
}
