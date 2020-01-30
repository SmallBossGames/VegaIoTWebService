using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VegaIoTApi.Migrations
{
    public partial class MoveDeviceAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoveDeviceDatas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(nullable: false),
                    BatteryLevel = table.Column<short>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    Reason = table.Column<short>(nullable: false),
                    Uptime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveDeviceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveDeviceDatas_TempDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "TempDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoveDeviceDatas_DeviceId",
                table: "MoveDeviceDatas",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoveDeviceDatas");
        }
    }
}
