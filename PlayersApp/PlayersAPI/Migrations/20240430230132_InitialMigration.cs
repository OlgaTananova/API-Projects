using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayersAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    playerID = table.Column<string>(type: "TEXT", nullable: false),
                    birthYear = table.Column<int>(type: "INTEGER", nullable: false),
                    birthMonth = table.Column<int>(type: "INTEGER", nullable: false),
                    birthDay = table.Column<int>(type: "INTEGER", nullable: false),
                    birthCountry = table.Column<string>(type: "TEXT", nullable: false),
                    birthState = table.Column<string>(type: "TEXT", nullable: true),
                    birthCity = table.Column<string>(type: "TEXT", nullable: true),
                    deathYear = table.Column<int>(type: "INTEGER", nullable: true),
                    deathMonth = table.Column<int>(type: "INTEGER", nullable: true),
                    deathDay = table.Column<int>(type: "INTEGER", nullable: true),
                    deathCountry = table.Column<string>(type: "TEXT", nullable: true),
                    deathState = table.Column<string>(type: "TEXT", nullable: true),
                    deathCity = table.Column<string>(type: "TEXT", nullable: true),
                    nameFirst = table.Column<string>(type: "TEXT", nullable: false),
                    nameLast = table.Column<string>(type: "TEXT", nullable: false),
                    nameGiven = table.Column<string>(type: "TEXT", nullable: false),
                    weight = table.Column<int>(type: "INTEGER", nullable: true),
                    height = table.Column<int>(type: "INTEGER", nullable: true),
                    bats = table.Column<char>(type: "TEXT", nullable: true),
                    throws = table.Column<char>(type: "TEXT", nullable: true),
                    debut = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    finalGame = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    retroID = table.Column<string>(type: "TEXT", nullable: true),
                    bbrefID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.playerID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
