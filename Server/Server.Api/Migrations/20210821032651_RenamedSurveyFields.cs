using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Api.Migrations
{
    public partial class RenamedSurveyFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Surveys",
                newName: "OpeningDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Surveys",
                newName: "ClosingDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OpeningDate",
                table: "Surveys",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ClosingDate",
                table: "Surveys",
                newName: "EndDate");
        }
    }
}
