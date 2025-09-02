using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Commercify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Devices and gadgets for everyday use", new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Electronics" },
                    { 2L, new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Apparel for men, women, and children", new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Clothing" },
                    { 3L, new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Appliances to improve home living", new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Home Appliances" },
                    { 4L, new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Literature across genres and interests", new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Books" },
                    { 5L, new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Toys and games for children of all ages", new DateTime(2024, 11, 28, 6, 0, 0, 0, DateTimeKind.Utc), "Toys" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
