using Application.Services.ProductServices.UpdateProductService.Interface;
using Domain.Input;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.UpdateProductService
{
    public class UpdateProductService : IUpdateProductService
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<bool> UpdateProduct(int id, ProductInput productInput)
        {
            //validar nome
            var productUpdate = _productRepository.UpdateProduct(id, productInput);

            return productUpdate;
        }
    }
}
