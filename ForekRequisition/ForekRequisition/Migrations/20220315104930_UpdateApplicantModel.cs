using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForekRequisition.Migrations
{
    public partial class UpdateApplicantModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProgrammeType",
                table: "Applicants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProgrammeType",
                table: "Applicants");
        }
    }
}
