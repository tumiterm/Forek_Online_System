using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForekRequisition.Migrations
{
    public partial class AddMedForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MedicalCert",
                table: "Education",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MedicalCert",
                table: "Education");
        }
    }
}
