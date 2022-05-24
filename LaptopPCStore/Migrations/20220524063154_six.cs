using Microsoft.EntityFrameworkCore.Migrations;

namespace LaptopPCStore.Migrations
{
    public partial class six : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_inventories",
                table: "inventories");

            migrationBuilder.DropIndex(
                name: "IX_inventories_lap_id",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "inv_id",
                table: "inventories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventories",
                table: "inventories",
                column: "lap_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_inventories",
                table: "inventories");

            migrationBuilder.AddColumn<int>(
                name: "inv_id",
                table: "inventories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_inventories",
                table: "inventories",
                column: "inv_id");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_lap_id",
                table: "inventories",
                column: "lap_id");
        }
    }
}
