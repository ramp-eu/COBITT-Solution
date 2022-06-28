using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dihweb.Migrations
{
    public partial class AddedOrderProcessTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProcess",
                columns: table => new
                {
                    OrderProcessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    DevicesId = table.Column<int>(type: "int", nullable: false),
                    DeviceStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProcess", x => x.OrderProcessId);
                    table.ForeignKey(
                        name: "FK_OrderProcess_Devices_DevicesId",
                        column: x => x.DevicesId,
                        principalTable: "Devices",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProcess_DeviceStatus_DeviceStatusId",
                        column: x => x.DeviceStatusId,
                        principalTable: "DeviceStatus",
                        principalColumn: "DeviceStatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProcess_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "OrdersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcess_DevicesId",
                table: "OrderProcess",
                column: "DevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcess_DeviceStatusId",
                table: "OrderProcess",
                column: "DeviceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProcess_OrdersId",
                table: "OrderProcess",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProcess");
        }
    }
}
