using Microsoft.EntityFrameworkCore.Migrations;

namespace VegaIoTApi.Migrations
{
    public partial class FixImpulsDevice1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpTime",
                table: "ImpulsDeviceDatas",
                newName: "Uptime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uptime",
                table: "ImpulsDeviceDatas",
                newName: "UpTime");
        }
    }
}
