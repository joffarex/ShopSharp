using System.Collections.Generic;
using ShopSharp.Application.StockAdmin.ViewModels;

namespace ShopSharp.Application.StockAdmin.Dto
{
    public class UpdateStockDto
    {
        public IEnumerable<StockWithProductViewModel> Stocks { get; set; }
    }
}