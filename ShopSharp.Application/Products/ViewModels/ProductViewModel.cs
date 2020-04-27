using System.Collections.Generic;

namespace ShopSharp.Application.Products.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}