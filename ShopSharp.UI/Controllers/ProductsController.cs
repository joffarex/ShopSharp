using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSharp.Application.ProductsAdmin;
using ShopSharp.Application.ProductsAdmin.Dto;

namespace ShopSharp.UI.Controllers
{
    [Route("[controller]")]
    [Authorize(Policy = "Manager")]
    public class ProductsController : Controller
    {
        [HttpGet("")]
        public IActionResult GetProducts([FromServices] GetProducts getProducts)
        {
            return Ok(getProducts.Exec());
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id, [FromServices] GetProduct getProduct)
        {
            return Ok(getProduct.Exec(id));
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto,
            [FromServices] CreateProduct createProduct)
        {
            return Ok(await createProduct.ExecAsync(productDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto,
            [FromServices] UpdateProduct updateProduct)
        {
            return Ok(await updateProduct.ExecAsync(id, productDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id, [FromServices] DeleteProduct deleteProduct)
        {
            return Ok(await deleteProduct.ExecAsync(id));
        }
    }
}