using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaIoTApi.Migrations
{
    public partial class TemperatureDevices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TempDevices",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Eui = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TempDeviceData",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DeviceId = table.Column<string>(nullable: false),
                    BatteryLevel = table.Column<short>(nullable: false),
                    PushTheLimit = table.Column<bool>(nullable: false),
                    Uptime = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<short>(nullable: false),
                    LowLimit = table.Column<short>(nullable: false),
                    HighLimit = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempDeviceData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TempDeviceData_TempDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "TempDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TempDeviceData_DeviceId",
                table: "TempDeviceData",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_TempDevices_Eui",
                table: "TempDevices",
                column: "Eui");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TempDeviceData");

            migrationBuilder.DropTable(
                name: "TempDevices");
        }
    }
}
