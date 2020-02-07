using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VegaIoTApi.Migrations
{
    public partial class AddImpulsDevice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ImpulsDeviceDatas",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeviceId = table.Column<long>(nullable: false),
                    PackageType = table.Column<byte>(nullable: false),
                    BatteryLevel = table.Column<short>(nullable: false),
                    MainSettings = table.Column<byte>(nullable: false),
                    AlarmExit = table.Column<byte>(nullable: false),
                    UpTime = table.Column<DateTimeOffset>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    InputState1 = table.Column<long>(nullable: false),
                    InputState2 = table.Column<long>(nullable: false),
                    InputState3 = table.Column<long>(nullable: false),
                    InputState4 = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpulsDeviceDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImpulsDeviceDatas_TempDevices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "TempDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImpulsDeviceDatas_DeviceId",
                table: "ImpulsDeviceDatas",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImpulsDeviceDatas");
        }
    }
}
