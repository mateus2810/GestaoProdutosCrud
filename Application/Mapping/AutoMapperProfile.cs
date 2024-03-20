using AutoMapper;
using Domain.DTO;
using Domain.Model;
using System.Collections.Generic;

namespace Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProductModel, ProductDTO>(); // Mapeia a classe de domínio ProductModel para a classe DTO ProductDTO
        }
    }
}