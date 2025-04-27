using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerRelationshipManagment.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Frequency = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    DateLastMowed = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    DateScheduled = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Acres = table.Column<double>(type: "REAL", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    RecentHoursMowing = table.Column<int>(type: "INTEGER", nullable: true),
                    HistoricHoursMowing = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Leads",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Frequency = table.Column<string>(type: "TEXT", nullable: false),
                    QuotePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    Acres = table.Column<double>(type: "REAL", nullable: false),
                    DateScheduled = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    Notes = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leads", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Acres", "Address", "DateLastMowed", "DateScheduled", "Frequency", "HistoricHoursMowing", "Name", "Notes", "Phone", "Price", "RecentHoursMowing" },
                values: new object[] { 2, 2.5, "123 Elm St", new DateOnly(2025, 4, 20), new DateOnly(2025, 5, 4), "Weekly", "[5,6,4]", "jane Doe", "First-time customer", "123-456-7890", 150.00m, 5 });

            migrationBuilder.InsertData(
                table: "Leads",
                columns: new[] { "Id", "Acres", "Address", "DateScheduled", "Frequency", "Name", "Notes", "Phone", "QuotePrice" },
                values: new object[] { 1, 3.0, "456 Oak St", new DateOnly(2025, 5, 11), "Bi-Weekly", "John Smith", "Interested in lawn care services", "987-654-3210", 200.00m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Leads");
        }
    }
}
