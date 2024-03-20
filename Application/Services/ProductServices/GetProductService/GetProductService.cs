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

        public GetProductService(IProductRepository productRepository, IMapper mapper) // Adicione IMapper ao construtor
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDTO> GetAllProduct()
        {
            var products = await _productRepository.GetAllProduct();

            // Use AutoMapper para mapear a lista de objetos obtidos do repositório para uma lista de DTOs
            var productDTOs = _mapper.Map<List<ProductDTO>>(products);

            // Você pode manipular ou processar a lista de DTOs aqui, se necessário

            // Por enquanto, vamos apenas retornar o primeiro produto da lista como exemplo
            return productDTOs.FirstOrDefault();
        }
    }
}
