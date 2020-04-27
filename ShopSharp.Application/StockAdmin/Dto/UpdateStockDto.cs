using System.Collections.Generic;
using ShopSharp.Application.StockAdmin.ViewModels;

namespace ShopSharp.Application.StockAdmin.Dto
{
    public class UpdateStockDto
    {
        public IEnumerable<StockViewModel> Stocks { get; set; }
    }
}