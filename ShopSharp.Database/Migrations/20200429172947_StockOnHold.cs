using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopSharp.Database.Migrations
{
    public partial class StockOnHold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "StocksOnHold",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(),
                    Quantity = table.Column<int>(),
                    ExpiryDate = table.Column<DateTime>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StocksOnHold", x => x.Id);
                    table.ForeignKey(
                        "FK_StocksOnHold_Stocks_StockId",
                        x => x.StockId,
                        "Stocks",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_StocksOnHold_StockId",
                "StocksOnHold",
                "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "StocksOnHold");
        }
    }
}