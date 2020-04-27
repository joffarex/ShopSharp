using System.Collections.Generic;

namespace ShopSharp.Application.StockAdmin.ViewModels
{
    public class UpdateStockViewModel
    {
        public IEnumerable<StockWithProductViewModel> Stocks { get; set; }
    }
}