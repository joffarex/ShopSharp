using System.Collections.Generic;

namespace ShopSharp.Application.StockAdmin.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}