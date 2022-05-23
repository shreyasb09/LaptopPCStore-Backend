using Microsoft.EntityFrameworkCore.Migrations;

namespace LaptopPCStore.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "inventories",
                newName: "inv_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "inv_id",
                table: "inventories",
                newName: "id");
        }
    }
}
