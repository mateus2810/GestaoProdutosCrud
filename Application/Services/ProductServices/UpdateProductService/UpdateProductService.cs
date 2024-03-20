using Application.Services.ProductServices.UpdateProductService.Interface;
using Application.Services.Validations;
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
        public async Task<(bool Success, string ErrorMessage)> UpdateProduct(int id, ProductInput productInput)
        {
            try
            {
                ProductValidation.Validate(productInput);
                //validar nome
                var productUpdate = await _productRepository.UpdateProduct(id, productInput);

                return (true, null); // Operação bem-sucedida
            }
            catch (Exception ex)
            {
                return (false, ex.Message); // Falha na validação
            }
        }
    }
}
