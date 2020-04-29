using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopSharp.Database.Migrations
{
    public partial class StockOnHoldSessionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "SessionId",
                "StocksOnHold",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "SessionId",
                "StocksOnHold");
        }
    }
}