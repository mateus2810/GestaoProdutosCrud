using Application.Services.ProductServices.GetProductService.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGetProductService _getProductService;
        public ProductController(IGetProductService getProductService)
        {
            _getProductService = getProductService;
        }

        // Rota GET: api/Exemplo
        [HttpGet]
        public async Task<IActionResult> Get() // Marcar o método como async
        {
            var retorno = await _getProductService.GetAllProduct();
            return Ok("Olá, mundo!, o retorno é :" + retorno.Id);
        }
    }
}