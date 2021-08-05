using Microsoft.EntityFrameworkCore.Migrations;

namespace Server.Api.Migrations
{
    public partial class OneToManyBetweenTeamAndCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Team_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Team_Courses_CourseId",
                table: "Team");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Team",
                table: "Team");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "Team",
                newName: "Teams");

            migrationBuilder.RenameIndex(
                name: "IX_Team_CourseId",
                table: "Teams",
                newName: "IX_Teams_CourseId");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Teams",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Courses_CourseId",
                table: "Teams",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Courses_CourseId",
                table: "Teams");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.RenameTable(
                name: "Teams",
                newName: "Team");

            migrationBuilder.RenameIndex(
                name: "IX_Teams_CourseId",
                table: "Team",
                newName: "IX_Team_CourseId");

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Team",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Team",
                table: "Team",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Team_TeamId",
                table: "AspNetUsers",
                column: "TeamId",
                principalTable: "Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Team_Courses_CourseId",
                table: "Team",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
