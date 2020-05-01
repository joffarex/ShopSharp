namespace ShopSharp.Application.Cart.Dto
{
    public class CartDto
    {
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public bool All { get; set; }
    }
}