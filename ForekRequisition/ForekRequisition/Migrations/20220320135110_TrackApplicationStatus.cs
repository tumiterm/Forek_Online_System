using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForekRequisition.Migrations
{
    public partial class TrackApplicationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApplicationComplete",
                table: "Education",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsIncomplete",
                table: "Education",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPartiallyComplete",
                table: "Education",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApplicationComplete",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "IsIncomplete",
                table: "Education");

            migrationBuilder.DropColumn(
                name: "IsPartiallyComplete",
                table: "Education");
        }
    }
}
