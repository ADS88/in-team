using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Api.Migrations
{
    public partial class LinkedSurveyToIteration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IterationId",
                table: "Surveys",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_IterationId",
                table: "Surveys",
                column: "IterationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Iterations_IterationId",
                table: "Surveys",
                column: "IterationId",
                principalTable: "Iterations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Iterations_IterationId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_IterationId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "IterationId",
                table: "Surveys");
        }
    }
}
