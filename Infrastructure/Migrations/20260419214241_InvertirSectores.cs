using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InvertirSectores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EventDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Venue = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sectors_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SectorId = table.Column<int>(type: "integer", nullable: false),
                    RowIdentifier = table.Column<string>(type: "text", nullable: false),
                    SeatNumber = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Sectors_SectorId",
                        column: x => x.SectorId,
                        principalTable: "Sectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { 1, new DateTime(2026, 12, 10, 21, 0, 0, 0, DateTimeKind.Utc), "Concierto de Rock", "Active", "Estadio Central" });

            migrationBuilder.InsertData(
                table: "Sectors",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 50, 1, "Platea Baja", 5000m },
                    { 2, 50, 1, "Platea Alta", 8000m }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0001-000000000001"), "Baja", 1, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000002"), "Baja", 2, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000003"), "Baja", 3, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000004"), "Baja", 4, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000005"), "Baja", 5, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000006"), "Baja", 6, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000007"), "Baja", 7, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000008"), "Baja", 8, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000009"), "Baja", 9, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000010"), "Baja", 10, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000011"), "Baja", 11, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000012"), "Baja", 12, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000013"), "Baja", 13, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000014"), "Baja", 14, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000015"), "Baja", 15, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000016"), "Baja", 16, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000017"), "Baja", 17, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000018"), "Baja", 18, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000019"), "Baja", 19, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000020"), "Baja", 20, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000021"), "Baja", 21, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000022"), "Baja", 22, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000023"), "Baja", 23, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000024"), "Baja", 24, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000025"), "Baja", 25, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000026"), "Baja", 26, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000027"), "Baja", 27, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000028"), "Baja", 28, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000029"), "Baja", 29, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000030"), "Baja", 30, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000031"), "Baja", 31, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000032"), "Baja", 32, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000033"), "Baja", 33, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000034"), "Baja", 34, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000035"), "Baja", 35, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000036"), "Baja", 36, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000037"), "Baja", 37, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000038"), "Baja", 38, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000039"), "Baja", 39, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000040"), "Baja", 40, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000041"), "Baja", 41, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000042"), "Baja", 42, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000043"), "Baja", 43, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000044"), "Baja", 44, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000045"), "Baja", 45, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000046"), "Baja", 46, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000047"), "Baja", 47, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000048"), "Baja", 48, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000049"), "Baja", 49, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0001-000000000050"), "Baja", 50, 1, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000051"), "Alta", 51, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000052"), "Alta", 52, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000053"), "Alta", 53, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000054"), "Alta", 54, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000055"), "Alta", 55, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000056"), "Alta", 56, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000057"), "Alta", 57, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000058"), "Alta", 58, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000059"), "Alta", 59, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000060"), "Alta", 60, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000061"), "Alta", 61, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000062"), "Alta", 62, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000063"), "Alta", 63, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000064"), "Alta", 64, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000065"), "Alta", 65, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000066"), "Alta", 66, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000067"), "Alta", 67, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000068"), "Alta", 68, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000069"), "Alta", 69, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000070"), "Alta", 70, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000071"), "Alta", 71, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000072"), "Alta", 72, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000073"), "Alta", 73, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000074"), "Alta", 74, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000075"), "Alta", 75, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000076"), "Alta", 76, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000077"), "Alta", 77, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000078"), "Alta", 78, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000079"), "Alta", 79, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000080"), "Alta", 80, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000081"), "Alta", 81, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000082"), "Alta", 82, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000083"), "Alta", 83, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000084"), "Alta", 84, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000085"), "Alta", 85, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000086"), "Alta", 86, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000087"), "Alta", 87, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000088"), "Alta", 88, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000089"), "Alta", 89, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000090"), "Alta", 90, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000091"), "Alta", 91, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000092"), "Alta", 92, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000093"), "Alta", 93, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000094"), "Alta", 94, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000095"), "Alta", 95, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000096"), "Alta", 96, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000097"), "Alta", 97, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000098"), "Alta", 98, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000099"), "Alta", 99, 2, "Available", 1 },
                    { new Guid("00000000-0000-0000-0002-000000000100"), "Alta", 100, 2, "Available", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_SectorId",
                table: "Seats",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Sectors_EventId",
                table: "Sectors",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Sectors");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
