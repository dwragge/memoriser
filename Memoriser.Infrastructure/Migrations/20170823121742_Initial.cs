using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Memoriser.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepetitionIntervals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EasinessFactor = table.Column<float>(type: "real", nullable: false),
                    Interval = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepetitionIntervals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IntervalId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ToBeGuessed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedAnswers = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningItems_RepetitionIntervals_IntervalId",
                        column: x => x.IntervalId,
                        principalTable: "RepetitionIntervals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningItems_IntervalId",
                table: "LearningItems",
                column: "IntervalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LearningItems");

            migrationBuilder.DropTable(
                name: "RepetitionIntervals");
        }
    }
}
