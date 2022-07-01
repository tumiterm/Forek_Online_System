using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForekRequisition.Migrations
{
    public partial class SignedOffer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasAcceptedOffer",
                table: "Applicants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasAcceptedOffer",
                table: "Applicants");
        }
    }
}
