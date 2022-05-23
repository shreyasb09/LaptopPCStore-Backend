using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LaptopPCStore.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lap_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "laptops",
                columns: table => new
                {
                    lap_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    lap_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_cat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_ram = table.Column<int>(type: "int", nullable: false),
                    lap_cpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_gpu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_disp_size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_disp_res = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_disp_rr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_battrey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_storage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_laptops", x => x.lap_id);
                });

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    ven_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ven_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ven_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ven_mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ven_address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendors", x => x.ven_id);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    invoice_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lap_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    inv_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    inv_phone = table.Column<int>(type: "int", nullable: false),
                    inv_mail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    inv_address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_invoices", x => x.invoice_id);
                    table.ForeignKey(
                        name: "FK_invoices_laptops_lap_id",
                        column: x => x.lap_id,
                        principalTable: "laptops",
                        principalColumn: "lap_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchases",
                columns: table => new
                {
                    purchase_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ven_id = table.Column<int>(type: "int", nullable: true),
                    lap_id = table.Column<int>(type: "int", nullable: true),
                    purchase_quantity = table.Column<int>(type: "int", nullable: false),
                    purchase_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    purchase_cost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_purchases", x => x.purchase_id);
                    table.ForeignKey(
                        name: "FK_purchases_laptops_lap_id",
                        column: x => x.lap_id,
                        principalTable: "laptops",
                        principalColumn: "lap_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_purchases_vendors_ven_id",
                        column: x => x.ven_id,
                        principalTable: "vendors",
                        principalColumn: "ven_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_invoices_lap_id",
                table: "invoices",
                column: "lap_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchases_lap_id",
                table: "purchases",
                column: "lap_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchases_ven_id",
                table: "purchases",
                column: "ven_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventories");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "purchases");

            migrationBuilder.DropTable(
                name: "laptops");

            migrationBuilder.DropTable(
                name: "vendors");
        }
    }
}
