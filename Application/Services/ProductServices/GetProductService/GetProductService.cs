using Application.Services.ProductServices.GetProductService.Interface;
using AutoMapper;
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
        private readonly IMapper _mapper;

        public GetProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<List<ProductDTO>> GetAllProduct()
        {
            var products = await _productRepository.GetAllProduct();

            var productDTOs = _mapper.Map<List<ProductDTO>>(products);

            return productDTOs;
        }

        public async Task<List<ProductSupplierDTO>> GetProductByCode(string codigo)
        {
            var products = await _productRepository.GetProductByCode(codigo);

            var productDTOs = _mapper.Map<List<ProductSupplierDTO>>(products);
            
            return productDTOs;
        }
    }
}
