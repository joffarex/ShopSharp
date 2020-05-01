namespace ShopSharp.Domain.Models
{
    public class CartProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public decimal Value { get; set; }
    }
}