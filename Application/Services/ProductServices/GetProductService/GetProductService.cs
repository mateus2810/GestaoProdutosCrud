using Application.Services.ProductServices.GetProductService.Interface;
using Domain.DTO;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.GetProductService
{
    public class GetProductService : IGetProductService
    {
        private readonly IProductRepository _productRepository;
        public async Task<ProductDTO> GetAllProduct()
        {
            var retorno = _productRepository.GetAllProduct();
            
            var product = new ProductDTO() { Id = 15};               
            return product;
        }
    }
}
