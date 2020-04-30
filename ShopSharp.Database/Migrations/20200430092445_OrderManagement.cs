using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopSharp.Database.Migrations
{
    public partial class OrderManagement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "Status",
                "Orders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Status",
                "Orders");
        }
    }
}