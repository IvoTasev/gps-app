using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gps_app.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceData_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "DeviceType" },
                values: new object[,]
                {
                    { "D-1567", "Aircraft" },
                    { "D-1568", "Personal" },
                    { "D-1569", "Asset" },
                    { "D-1570", "Personal" },
                    { "D-1571", "Asset" }
                });

            migrationBuilder.InsertData(
                table: "DeviceData",
                columns: new[] { "Id", "Date", "DeviceId", "Location" },
                values: new object[,]
                {
                    { "1", new DateTime(2022, 8, 31, 10, 5, 0, 0, DateTimeKind.Unspecified), "D-1567", "L1" },
                    { "10", new DateTime(2022, 8, 31, 10, 25, 0, 0, DateTimeKind.Unspecified), "D-1568", "L3" },
                    { "11", new DateTime(2022, 8, 31, 10, 15, 0, 0, DateTimeKind.Unspecified), "D-1569", "L4" },
                    { "12", new DateTime(2022, 8, 31, 10, 20, 0, 0, DateTimeKind.Unspecified), "D-1569", "L4" },
                    { "13", new DateTime(2022, 8, 31, 10, 30, 0, 0, DateTimeKind.Unspecified), "D-1569", "L1" },
                    { "14", new DateTime(2022, 8, 31, 10, 35, 0, 0, DateTimeKind.Unspecified), "D-1569", "L1" },
                    { "15", new DateTime(2022, 8, 31, 10, 35, 0, 0, DateTimeKind.Unspecified), "D-1570", "L5" },
                    { "16", new DateTime(2022, 8, 31, 10, 35, 0, 0, DateTimeKind.Unspecified), "D-1571", "L6" },
                    { "2", new DateTime(2022, 8, 31, 10, 10, 0, 0, DateTimeKind.Unspecified), "D-1567", "L1" },
                    { "3", new DateTime(2022, 8, 31, 10, 15, 0, 0, DateTimeKind.Unspecified), "D-1567", "L1" },
                    { "4", new DateTime(2022, 8, 31, 10, 20, 0, 0, DateTimeKind.Unspecified), "D-1567", "L1" },
                    { "5", new DateTime(2022, 8, 31, 10, 25, 0, 0, DateTimeKind.Unspecified), "D-1567", "L2" },
                    { "6", new DateTime(2022, 8, 31, 10, 5, 0, 0, DateTimeKind.Unspecified), "D-1568", "L3" },
                    { "7", new DateTime(2022, 8, 31, 10, 10, 0, 0, DateTimeKind.Unspecified), "D-1568", "L3" },
                    { "8", new DateTime(2022, 8, 31, 10, 15, 0, 0, DateTimeKind.Unspecified), "D-1568", "L3" },
                    { "9", new DateTime(2022, 8, 31, 10, 20, 0, 0, DateTimeKind.Unspecified), "D-1568", "L3" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceData_DeviceId",
                table: "DeviceData",
                column: "DeviceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceData");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
