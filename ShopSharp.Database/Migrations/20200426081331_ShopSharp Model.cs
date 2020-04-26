using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopSharp.Database.Migrations
{
    public partial class ShopSharpModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Orders",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderRef = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Orders", x => x.Id); });

            migrationBuilder.CreateTable(
                "Stocks",
                table => new
                {
                    Id = table.Column<int>()
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(),
                    ProductId = table.Column<int>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        "FK_Stocks_Products_ProductId",
                        x => x.ProductId,
                        "Products",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "OrderProducts",
                table => new
                {
                    ProductId = table.Column<int>(),
                    OrderId = table.Column<int>()
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

            migrationBuilder.CreateIndex(
                "IX_Stocks_ProductId",
                "Stocks",
                "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "OrderProducts");

            migrationBuilder.DropTable(
                "Stocks");

            migrationBuilder.DropTable(
                "Orders");
        }
    }
}