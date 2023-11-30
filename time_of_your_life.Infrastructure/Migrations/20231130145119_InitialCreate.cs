using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace time_of_your_life.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClockPresets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    TitleText = table.Column<string>(type: "TEXT", nullable: false),
                    FontFamily = table.Column<string>(type: "TEXT", nullable: false),
                    TitleFontSize = table.Column<int>(type: "INTEGER", nullable: false),
                    ClockFontSize = table.Column<int>(type: "INTEGER", nullable: false),
                    BlinkColons = table.Column<bool>(type: "INTEGER", nullable: false),
                    TitleFontColor = table.Column<string>(type: "TEXT", nullable: false),
                    ClockFontColor = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClockPresets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClockPresets");
        }
    }
}
