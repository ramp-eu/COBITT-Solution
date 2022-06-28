using Microsoft.EntityFrameworkCore.Migrations;

namespace dihweb.Migrations
{
    public partial class CreatedMachineOrderStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MachineOrderStatus",
                columns: table => new
                {
                    MachineOrderStatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineOrderStatus", x => x.MachineOrderStatusId);
                    table.ForeignKey(
                        name: "FK_MachineOrderStatus_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "OrdersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MachineOrderStatus_OrdersId",
                table: "MachineOrderStatus",
                column: "OrdersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MachineOrderStatus");
        }
    }
}
