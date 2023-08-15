using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaringCompare.Migrations
{
    /// <inheritdoc />
    public partial class INIT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tarings",
                columns: table => new
                {
                    TaringID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LitersMax = table.Column<int>(type: "int", nullable: false),
                    LevelMin = table.Column<int>(type: "int", nullable: false),
                    LevelMax = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SensorLength = table.Column<int>(type: "int", nullable: false),
                    DistanceX = table.Column<int>(type: "int", nullable: false),
                    DistanceY = table.Column<int>(type: "int", nullable: false),
                    HtmlPage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarings", x => x.TaringID);
                });

            migrationBuilder.CreateTable(
                name: "TaringItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<long>(type: "bigint", nullable: false),
                    Delta = table.Column<float>(type: "real", nullable: false),
                    RawLevel = table.Column<int>(type: "int", nullable: false),
                    LitersLevel = table.Column<float>(type: "real", nullable: false),
                    TaringID = table.Column<long>(type: "bigint", nullable: false),
                    TaringID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaringItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaringItem_Tarings_TaringID1",
                        column: x => x.TaringID1,
                        principalTable: "Tarings",
                        principalColumn: "TaringID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaringItem_TaringID1",
                table: "TaringItem",
                column: "TaringID1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaringItem");

            migrationBuilder.DropTable(
                name: "Tarings");
        }
    }
}
