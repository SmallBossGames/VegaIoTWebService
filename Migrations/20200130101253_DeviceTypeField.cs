using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaIoTApi.Migrations
{
    public partial class DeviceTypeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeviceType",
                table: "TempDevices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "TempDevices");
        }
    }
}
