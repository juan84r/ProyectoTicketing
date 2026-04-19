using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    { 1, 50, 1, "Platea Alta", 5000m },
                    { 2, 50, 1, "Campo VIP", 8000m }
                });

            migrationBuilder.InsertData(
                table: "Seats",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("00305094-5d6f-40b3-8cb2-d4cbe6c34009"), "VIP", 21, 2, "Available", 1 },
                    { new Guid("01cfaed1-e02e-45da-9bab-a15986625edd"), "A", 14, 1, "Available", 1 },
                    { new Guid("0327a27f-890f-4160-bcea-0d91462e786a"), "VIP", 15, 2, "Available", 1 },
                    { new Guid("032cba9f-3186-4d92-ac63-962d3488870e"), "VIP", 43, 2, "Available", 1 },
                    { new Guid("05633cff-aee4-4ea9-87f6-ce9b5743fe0b"), "VIP", 26, 2, "Available", 1 },
                    { new Guid("07dd1023-5941-4d55-8b99-c4668f0d1bfd"), "A", 41, 1, "Available", 1 },
                    { new Guid("083797e1-bd47-4a81-8f4a-abd97e006945"), "A", 35, 1, "Available", 1 },
                    { new Guid("0ccb793f-e46a-4154-b762-b00271242a7a"), "A", 36, 1, "Available", 1 },
                    { new Guid("0db4f331-bb07-48bd-9358-f29b6cfa6b9b"), "VIP", 50, 2, "Available", 1 },
                    { new Guid("0eded9f3-3dac-4a1b-a71d-42e786637730"), "A", 26, 1, "Available", 1 },
                    { new Guid("0f523381-97de-4310-9004-3aff3a407d52"), "A", 10, 1, "Available", 1 },
                    { new Guid("1138f4f0-f77c-420d-89d0-77fd300e5ea7"), "A", 11, 1, "Available", 1 },
                    { new Guid("130740e7-ece1-4f42-8e9a-dc7388cafd0d"), "VIP", 4, 2, "Available", 1 },
                    { new Guid("15c5a08b-3db6-4695-8e4a-39a0cfae9680"), "VIP", 31, 2, "Available", 1 },
                    { new Guid("17a02dd7-b517-45a9-8a64-790fe184c10b"), "VIP", 48, 2, "Available", 1 },
                    { new Guid("18cee384-df13-4f34-a39a-05bbf172311e"), "A", 18, 1, "Available", 1 },
                    { new Guid("1f3ab75b-d4c3-47d4-8316-cfe9bfab9e9c"), "VIP", 47, 2, "Available", 1 },
                    { new Guid("1f427e7e-8a6a-4306-ab0f-9dd1407819eb"), "A", 20, 1, "Available", 1 },
                    { new Guid("24bc3d49-0417-4132-accc-4d80940ff2a1"), "A", 19, 1, "Available", 1 },
                    { new Guid("26bb75a8-0f14-4548-9b14-2c53f26b4464"), "VIP", 35, 2, "Available", 1 },
                    { new Guid("26f20219-4808-444a-bac9-6aa7663d5c8e"), "VIP", 32, 2, "Available", 1 },
                    { new Guid("28f57ac4-01d6-4677-b97e-f88e752b015c"), "A", 5, 1, "Available", 1 },
                    { new Guid("29923a37-c62c-4a9c-8a63-2c1c20880427"), "A", 12, 1, "Available", 1 },
                    { new Guid("2db22338-00bb-4384-90cf-860110fd037c"), "A", 42, 1, "Available", 1 },
                    { new Guid("2fbe685a-d7dc-4dd0-a7a6-9195bce6234f"), "VIP", 40, 2, "Available", 1 },
                    { new Guid("3a3413d2-2645-40b2-8fef-7d1c9ba4ec2b"), "VIP", 1, 2, "Available", 1 },
                    { new Guid("3dedd651-52e8-4229-8d1b-373f958546c0"), "A", 45, 1, "Available", 1 },
                    { new Guid("3f276c44-4c0e-4fd7-a524-afb61e19696f"), "A", 28, 1, "Available", 1 },
                    { new Guid("457709aa-7162-4a30-905b-73822ceef5bd"), "A", 15, 1, "Available", 1 },
                    { new Guid("4cd59b7f-2f37-41fd-842e-6c5c46532f4e"), "VIP", 39, 2, "Available", 1 },
                    { new Guid("507543e5-826c-45aa-9233-e8cf2310b128"), "A", 44, 1, "Available", 1 },
                    { new Guid("5144ffe1-f650-483f-af96-2a7b38161633"), "VIP", 28, 2, "Available", 1 },
                    { new Guid("52ac5b4f-6826-477e-8fd9-978519fe2776"), "VIP", 24, 2, "Available", 1 },
                    { new Guid("59505aaa-3a32-4963-86e7-6a21de5e1b9f"), "A", 33, 1, "Available", 1 },
                    { new Guid("5b567927-3c96-4b8e-a327-7296a4fa9237"), "VIP", 20, 2, "Available", 1 },
                    { new Guid("5bd9feed-b90f-4025-8b16-f008d5485928"), "VIP", 23, 2, "Available", 1 },
                    { new Guid("6e6cc0da-60ad-43a9-b6ba-d74c9e413e52"), "VIP", 10, 2, "Available", 1 },
                    { new Guid("75e8d700-b851-4b88-8057-ea7d5756d1cf"), "A", 40, 1, "Available", 1 },
                    { new Guid("76d314bf-74b0-4a76-a2fa-a4177dde5d1a"), "A", 27, 1, "Available", 1 },
                    { new Guid("7e3a56bb-f4e1-4ed7-a755-4b4eac386508"), "VIP", 16, 2, "Available", 1 },
                    { new Guid("81eed048-c378-469c-89bd-f69f72dcb649"), "A", 31, 1, "Available", 1 },
                    { new Guid("84c3c73f-a707-474a-9b6c-7bac25d2982d"), "A", 46, 1, "Available", 1 },
                    { new Guid("85908078-add9-4af7-b768-9241aae02bfe"), "VIP", 44, 2, "Available", 1 },
                    { new Guid("85dd2e6c-e3af-4d2d-8a7b-c7980089313e"), "VIP", 22, 2, "Available", 1 },
                    { new Guid("8a77b200-d327-4079-a333-da14b5b444c5"), "A", 13, 1, "Available", 1 },
                    { new Guid("8e70f743-3d05-4632-9ee5-ee436ad679fc"), "A", 38, 1, "Available", 1 },
                    { new Guid("922728b8-b0e4-4df9-977b-5088b4b515a5"), "VIP", 38, 2, "Available", 1 },
                    { new Guid("959eff62-8e54-453a-8494-edbf7399f325"), "A", 7, 1, "Available", 1 },
                    { new Guid("95de5128-168e-4ab1-87cf-4e571bd06820"), "A", 6, 1, "Available", 1 },
                    { new Guid("98f4db95-87a4-4d84-b6c5-953ef795480a"), "A", 30, 1, "Available", 1 },
                    { new Guid("9af7c6d2-2ca9-426f-afad-26ecce1f2504"), "A", 48, 1, "Available", 1 },
                    { new Guid("9b4b1604-cfd5-43a9-82e6-70538e371003"), "VIP", 29, 2, "Available", 1 },
                    { new Guid("9c700c8f-cfa4-4ef3-af75-e6e6ae2219b8"), "VIP", 18, 2, "Available", 1 },
                    { new Guid("9e892484-64ae-441e-8ed6-1832c581c381"), "VIP", 27, 2, "Available", 1 },
                    { new Guid("a1164e10-87ac-42ba-8aba-e636dfcfccc0"), "VIP", 37, 2, "Available", 1 },
                    { new Guid("a46e7651-1f7a-40ed-976d-079c38bfb709"), "VIP", 2, 2, "Available", 1 },
                    { new Guid("a63d89bf-875e-405e-84b0-77a0630a97f9"), "VIP", 7, 2, "Available", 1 },
                    { new Guid("a6dc1d21-ce02-4a91-805a-e94a32caf59c"), "VIP", 36, 2, "Available", 1 },
                    { new Guid("a868621c-fc80-4603-8b63-dd29acd4d253"), "A", 47, 1, "Available", 1 },
                    { new Guid("ac48e58c-0471-41db-a266-9a3556409050"), "VIP", 25, 2, "Available", 1 },
                    { new Guid("ad123ca0-5457-4443-bd5c-35134ebf1c9c"), "A", 22, 1, "Available", 1 },
                    { new Guid("b2a167ef-075a-4609-b1e3-2f0880bd829d"), "VIP", 12, 2, "Available", 1 },
                    { new Guid("b45e0142-6912-4504-b061-c6c7b0f0c79a"), "A", 23, 1, "Available", 1 },
                    { new Guid("b5d4750f-e4ad-4e74-8b75-2bb9b1ac5b28"), "A", 50, 1, "Available", 1 },
                    { new Guid("bb68c033-02ff-4378-bbfe-b66fd66b6124"), "A", 25, 1, "Available", 1 },
                    { new Guid("bbc7403b-de10-48c9-88fb-2d5d6541fc43"), "VIP", 33, 2, "Available", 1 },
                    { new Guid("bd424e18-5d80-427a-97d9-4a1608cbdf8f"), "VIP", 3, 2, "Available", 1 },
                    { new Guid("bf0040c6-072f-4f9f-ab57-4d0e0ef55fd2"), "VIP", 41, 2, "Available", 1 },
                    { new Guid("c1711c5f-7c01-4e59-8694-aa8248450e15"), "VIP", 17, 2, "Available", 1 },
                    { new Guid("c261c4aa-4352-4be7-bd31-6928d62f14e2"), "VIP", 9, 2, "Available", 1 },
                    { new Guid("c39c9f2b-2f4e-4c6f-82fd-51582569adb1"), "A", 16, 1, "Available", 1 },
                    { new Guid("c4e34d2c-ee68-4936-9823-8094b53bec32"), "A", 43, 1, "Available", 1 },
                    { new Guid("c54f1275-8d08-47bd-b543-cd071ca3c398"), "VIP", 19, 2, "Available", 1 },
                    { new Guid("c6dd50cc-52ea-4555-8c8c-ff9e8295e88c"), "A", 32, 1, "Available", 1 },
                    { new Guid("c91137df-0cbd-416d-9f6e-51657edf8629"), "A", 24, 1, "Available", 1 },
                    { new Guid("ca69286f-6ab6-40fa-b3b4-b98dee3e4c96"), "A", 3, 1, "Available", 1 },
                    { new Guid("cb315371-8125-468d-9840-219ea091f775"), "A", 49, 1, "Available", 1 },
                    { new Guid("cbe06aa6-2724-495b-ac55-9d9934f616de"), "VIP", 13, 2, "Available", 1 },
                    { new Guid("d0197dec-83a8-46e9-a9db-dbd0bfd6e06e"), "A", 4, 1, "Available", 1 },
                    { new Guid("d1af248f-24a1-4ba7-9693-89d6f2489f26"), "VIP", 11, 2, "Available", 1 },
                    { new Guid("d1d358cb-305d-44bb-baa7-16f5067c84d3"), "A", 1, 1, "Available", 1 },
                    { new Guid("d1ddbcc9-912b-4ad8-8359-4a1b9bfca364"), "VIP", 5, 2, "Available", 1 },
                    { new Guid("d3985d8d-29b5-4749-80c2-5a0e5b66d15c"), "VIP", 14, 2, "Available", 1 },
                    { new Guid("d4f4870c-c482-4f67-870f-d991e383ebdc"), "VIP", 45, 2, "Available", 1 },
                    { new Guid("dbd7ba5f-d0a7-4a50-9d49-18962737f6a4"), "A", 21, 1, "Available", 1 },
                    { new Guid("dd31a3d9-8b13-4fa9-82fe-f4911055e1ed"), "VIP", 6, 2, "Available", 1 },
                    { new Guid("ddda7f39-5a30-4f0b-96a9-f6fb3cefc283"), "A", 34, 1, "Available", 1 },
                    { new Guid("defb99c8-980a-4a94-90b8-d982e5b4064b"), "VIP", 8, 2, "Available", 1 },
                    { new Guid("e3799c11-41e9-494b-b5e0-9fc91ba5fc51"), "A", 17, 1, "Available", 1 },
                    { new Guid("e3c490e6-429f-47fc-899e-4fd5757f3552"), "A", 37, 1, "Available", 1 },
                    { new Guid("e487d6be-3dfc-4cd5-a0c7-19a84e443402"), "VIP", 42, 2, "Available", 1 },
                    { new Guid("e7656ff6-2543-4a78-a1ad-d127bc8630cf"), "VIP", 49, 2, "Available", 1 },
                    { new Guid("ebb166d9-d0b2-4a08-b8af-f24b37036a20"), "A", 29, 1, "Available", 1 },
                    { new Guid("f407e825-4c9a-4346-9c7a-2fe98db899e6"), "A", 2, 1, "Available", 1 },
                    { new Guid("f60d256c-4f37-4b45-9ddf-02f5a4f764ac"), "A", 8, 1, "Available", 1 },
                    { new Guid("f661b7e5-7d4e-41df-a8f1-b175af0ead20"), "VIP", 46, 2, "Available", 1 },
                    { new Guid("f81cbcd6-4608-46db-8ef2-c34855ee08e3"), "A", 9, 1, "Available", 1 },
                    { new Guid("f8956db1-02d2-4719-bbfc-60f625af2d74"), "VIP", 34, 2, "Available", 1 },
                    { new Guid("f962c3ee-5ff4-44e5-9c37-1fee5ebeeddc"), "VIP", 30, 2, "Available", 1 },
                    { new Guid("fe5f1038-b820-47fc-b0aa-5d42863a9b17"), "A", 39, 1, "Available", 1 }
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
