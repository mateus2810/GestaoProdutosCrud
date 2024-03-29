﻿using Application.Services.ProductServices.CreateProductService.Interface;
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

        [HttpGet]
        public async Task<IActionResult> GetAllProduct(int pageNumber, int pageSize)
        {
            var products = await _getProductService.GetAllProduct(pageNumber, pageSize);

            if (products.Count == 0)
            {
                return NoContent();
            }
            return Ok(products);
        }


        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(ProductInput product)
        {
            var (success, errorMessage) = await _createProductServices.CreateProduct(product);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(errorMessage);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductInput product)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var (success, errorMessage) = await _updateProductService.UpdateProduct(id, product);

            if (success)
            {
                return NoContent(); 
            }
            else
            {
                return BadRequest(errorMessage); 
            }
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

            return NoContent();
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetProductByCode(string codigo)
        {
            if (string.IsNullOrEmpty(codigo))
            {
                return BadRequest("Código não fornecido.");
            }

            var product = await _getProductService.GetProductByCode(codigo);

            if (product == null)
            {
                return NotFound($"Produto com código '{codigo}' não encontrado.");
            }

            return Ok(product);
        }

    }
}
