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
            CreateMap<SupplierModel, SupplierDTO>(); // Mapeia a classe de domínio SupplierModel para a classe DTO SupplierDTO
            CreateMap<ProductSupplierModel, ProductSupplierDTO>(); // Mapeia a classe de domínio ProductSupplierModel para a classe DTO ProductSupplierDTO
        }
    }
}