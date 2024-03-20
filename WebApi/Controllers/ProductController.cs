using Application.Services.ProductServices.CreateProductService.Interface;
using Application.Services.ProductServices.GetProductService.Interface;
using Domain.DTO;
using Domain.Input;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IGetProductService _getProductService;
        private readonly ICreateProductServices _createProductServices;
        public ProductController(
            IGetProductService getProductService,
            ICreateProductServices createProductServices)
        {
            _getProductService = getProductService;
            _createProductServices = createProductServices;
        }

        // Rota GET: api/Exemplo
        [HttpGet]
        public async Task<IActionResult> GetAllProduct() // Marcar o método como async
        {
            var products = await _getProductService.GetAllProduct();

            //verificar possivel melhoria metodo
            if (products.Count == 0)
            {
                return NoContent();
            }
            return Ok(products);
        }


        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductInput product)
        {
            //vverificar sobre await classe
            var addedProduct = await _createProductServices.CreateProduct(product);
            
            return Ok(addedProduct);

            //return CreatedAtAction(nameof(GetProductById), new { id = addedProduct.Id }, addedProduct);
        }
    }
}