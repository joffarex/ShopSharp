using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopSharp.Database.Migrations
{
    public partial class OrderStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "OrderProducts");

            migrationBuilder.AddColumn<string>(
                "Email",
                "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "FirstName",
                "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "LastName",
                "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "PhoneNumber",
                "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                "StripeRef",
                "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                "OrderStocks",
                table => new
                {
                    OrderId = table.Column<int>(),
                    StockId = table.Column<int>(),
                    Quantity = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStocks", x => new {x.StockId, x.OrderId});
                    table.ForeignKey(
                        "FK_OrderStocks_Orders_OrderId",
                        x => x.OrderId,
                        "Orders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_OrderStocks_Stocks_StockId",
                        x => x.StockId,
                        "Stocks",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_OrderStocks_OrderId",
                "OrderStocks",
                "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "OrderStocks");

            migrationBuilder.DropColumn(
                "Email",
                "Orders");

            migrationBuilder.DropColumn(
                "FirstName",
                "Orders");

            migrationBuilder.DropColumn(
                "LastName",
                "Orders");

            migrationBuilder.DropColumn(
                "PhoneNumber",
                "Orders");

            migrationBuilder.DropColumn(
                "StripeRef",
                "Orders");

            migrationBuilder.CreateTable(
                "OrderProducts",
                table => new
                {
                    ProductId = table.Column<int>("int"),
                    OrderId = table.Column<int>("int")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new {x.ProductId, x.OrderId});
                    table.ForeignKey(
                        "FK_OrderProducts_Orders_OrderId",
                        x => x.OrderId,
                        "Orders",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_OrderProducts_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_OrderProducts_OrderId",
                "OrderProducts",
                "OrderId");
        }
    }
}