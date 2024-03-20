using Application.Services.ProductServices.CreateProductService.Interface;
using Application.Services.ProductServices.DeleteProductService.Interface;
using Application.Services.ProductServices.GetProductService.Interface;
using Application.Services.ProductServices.UpdateProductService.Interface;
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
        private readonly IUpdateProductService _updateProductService;
        private readonly IDeleteProductService _deleteProductService;

        public ProductController(
            IGetProductService getProductService,
            ICreateProductServices createProductServices,
            IUpdateProductService updateProductService,
            IDeleteProductService deleteProductService)
        {
            _getProductService = getProductService;
            _createProductServices = createProductServices;
            _updateProductService = updateProductService;
            _deleteProductService = deleteProductService;
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
            var addedProduct = await _createProductServices.CreateProduct(product);
            
            return Ok(addedProduct);
        }


        //VERIFICAR VALIDAÇÕES DE ENTRADA
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductInput product)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var success = await _updateProductService.UpdateProduct(id, product);
            
            if (!success)
            {
                return NotFound();
            }

            //avaliar
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var success = await _deleteProductService.DeleteProduct(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent(); // Retornar 204 (No Content) se o produto foi deletado com sucesso
        }

    }
}