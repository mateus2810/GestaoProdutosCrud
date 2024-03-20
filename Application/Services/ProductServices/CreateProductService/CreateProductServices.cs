using Application.Services.ProductServices.CreateProductService.Interface;
using Application.Services.Validations;
using Domain.Input;
using Domain.Interfaces;
using System;
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


        public async Task<(bool Success, string ErrorMessage)> CreateProduct(ProductInput productInput)
        {
            try
            {
                ProductValidation.Validate(productInput);
                var addedProduct = await _productRepository.CreateProduct(productInput);
                
                return (true, null);
            }
            catch (ArgumentException ex)
            {
                return (false, ex.Message);
            }
        }
    }
}

