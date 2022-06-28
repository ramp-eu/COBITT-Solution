using Microsoft.EntityFrameworkCore.Migrations;

namespace dihweb.Migrations
{
    public partial class AddedDeviceTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceType",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeviceTypeID",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeviceType",
                columns: table => new
                {
                    DeviceTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceType", x => x.DeviceTypeID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeID",
                table: "Devices",
                column: "DeviceTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceType_DeviceTypeID",
                table: "Devices",
                column: "DeviceTypeID",
                principalTable: "DeviceType",
                principalColumn: "DeviceTypeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceType_DeviceTypeID",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceType");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceTypeID",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceTypeID",
                table: "Devices");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "DeviceType",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
