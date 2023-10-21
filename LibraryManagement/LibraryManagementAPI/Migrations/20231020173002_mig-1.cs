using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    CheckedOut = table.Column<bool>(type: "boolean", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("03ed0716-89d8-4267-9fa1-44271ffd3339"), "Aldous Huxley" },
                    { new Guid("87a25f21-2190-4c16-bb39-86fbd6da0de6"), "Mary Shelley" },
                    { new Guid("d0be27e1-4515-4f81-a919-b1204ef58b10"), "Ray Bradbury" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CheckedOut", "ISBN", "Title" },
                values: new object[,]
                {
                    { new Guid("0e344965-9852-4c8b-a0b0-4f1e2c8fd8e3"), new Guid("03ed0716-89d8-4267-9fa1-44271ffd3339"), false, "0731234570", "The Art of Seeing" },
                    { new Guid("1eb2c51f-4b19-4a95-9bbf-90cb6a23d218"), new Guid("87a25f21-2190-4c16-bb39-86fbd6da0de6"), false, "0735619688", "Frankenstein" },
                    { new Guid("550e8400-e29b-41d4-a716-446655440000"), new Guid("03ed0716-89d8-4267-9fa1-44271ffd3339"), false, "0735619444", "Brave New World" },
                    { new Guid("6b29fc40-ca47-1067-b31d-00dd010662da"), new Guid("d0be27e1-4515-4f81-a919-b1204ef58b10"), false, "0735619670", "The Halloween Tree" },
                    { new Guid("f47ac10b-58cc-4372-a567-0e02b2c3d479"), new Guid("d0be27e1-4515-4f81-a919-b1204ef58b10"), false, "0451524934", "Fahrenheit 451" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
