using Microsoft.EntityFrameworkCore.Migrations;

namespace dihweb.Migrations
{
    public partial class UpdatedMachineOrderStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DevicesId",
                table: "MachineOrderStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MachineOrderStatus_DevicesId",
                table: "MachineOrderStatus",
                column: "DevicesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineOrderStatus_Devices_DevicesId",
                table: "MachineOrderStatus",
                column: "DevicesId",
                principalTable: "Devices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineOrderStatus_Devices_DevicesId",
                table: "MachineOrderStatus");

            migrationBuilder.DropIndex(
                name: "IX_MachineOrderStatus_DevicesId",
                table: "MachineOrderStatus");

            migrationBuilder.DropColumn(
                name: "DevicesId",
                table: "MachineOrderStatus");
        }
    }
}
