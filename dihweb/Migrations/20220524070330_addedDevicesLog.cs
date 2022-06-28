using Microsoft.EntityFrameworkCore.Migrations;

namespace dihweb.Migrations
{
    public partial class addedDevicesLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevicesLog",
                columns: table => new
                {
                    DevicesLogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceStatusId = table.Column<int>(type: "int", nullable: false),
                    DevicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicesLog", x => x.DevicesLogId);
                    table.ForeignKey(
                        name: "FK_DevicesLog_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DevicesLog_DeviceStatus_DeviceStatusId",
                        column: x => x.DeviceStatusId,
                        principalTable: "DeviceStatus",
                        principalColumn: "DeviceStatusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevicesLog_DevicesId",
                table: "DevicesLog",
                column: "DevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_DevicesLog_DeviceStatusId",
                table: "DevicesLog",
                column: "DeviceStatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevicesLog");
        }
    }
}
