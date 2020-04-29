using System;

namespace ShopSharp.Domain.Models
{
    public class StockOnHold
    {
        public int Id { get; set; }

        public string SessionId { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }

        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}