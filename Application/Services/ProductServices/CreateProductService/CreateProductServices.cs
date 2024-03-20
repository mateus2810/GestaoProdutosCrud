using Application.Services.ProductServices.CreateProductService.Interface;
using Domain.Input;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Application.Services.CreateProductService
{
    public  class CreateProductServices : ICreateProductServices
    {
        private readonly IProductRepository _productRepository;
        public CreateProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        public async Task<bool> CreateProduct(ProductInput productInput)
        {
            var product = await _productRepository.InsertProduct(productInput);

            return product;
        }
    }
}
