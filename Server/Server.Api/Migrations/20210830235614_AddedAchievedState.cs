using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Server.Api.Migrations
{
    public partial class AddedAchievedState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AchievedStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    StateId = table.Column<int>(type: "integer", nullable: false),
                    AlphaId = table.Column<int>(type: "integer", nullable: false),
                    AchievedDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    IterationId = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AchievedStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AchievedStates_Alphas_AlphaId",
                        column: x => x.AlphaId,
                        principalTable: "Alphas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievedStates_Iterations_IterationId",
                        column: x => x.IterationId,
                        principalTable: "Iterations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievedStates_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AchievedStates_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AchievedStates_AlphaId",
                table: "AchievedStates",
                column: "AlphaId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievedStates_IterationId",
                table: "AchievedStates",
                column: "IterationId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievedStates_StateId",
                table: "AchievedStates",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_AchievedStates_TeamId",
                table: "AchievedStates",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AchievedStates");
        }
    }
}
