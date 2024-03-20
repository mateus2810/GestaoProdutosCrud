using Application.Services.ProductServices.DeleteProductService.Interface;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.DeleteProductService
{
    public class DeleteProductService : IDeleteProductService
    {
        readonly private IProductRepository _productRepository;

        public DeleteProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Task<bool> DeleteProduct(int id)
        {
            var productDelete = _productRepository.DeleteProduct(id);

            return productDelete;
        }
    }
}
