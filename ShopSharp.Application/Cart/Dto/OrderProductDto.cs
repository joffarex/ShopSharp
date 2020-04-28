namespace ShopSharp.Application.Cart.Dto
{
    public class OrderProductDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int StockId { get; set; }
        public int Value { get; set; }
    }
}