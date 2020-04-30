namespace ShopSharp.Application.Cart.ViewModels
{
    public class CartViewModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public decimal RealValue { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
    }
}