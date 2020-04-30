using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.StockAdmin;
using ShopSharp.Application.StockAdmin.Dto;
using ShopSharp.Database;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult GetStock([FromServices] GetStock getStock)
        {
            return Ok(getStock.Exec());
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateStock([FromBody] StockDto stockDto,
            [FromServices] CreateStock createStock)
        {
            return Ok(await createStock.ExecAsync(stockDto));
        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateStockDto updateStockDto,
            [FromServices] UpdateStock updateStock)
        {
            return Ok(await updateStock.ExecAsync(updateStockDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock(int id, [FromServices] DeleteStock deleteStock)
        {
            return Ok(await deleteStock.ExecAsync(id));
        }
    }
}