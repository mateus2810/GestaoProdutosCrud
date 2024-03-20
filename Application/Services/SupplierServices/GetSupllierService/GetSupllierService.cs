using Application.Services.SupplierServices.GetSupllierService.Interface;
using AutoMapper;
using Domain.DTO;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.GetSupllierService
{
    public class GetSupllierService : IGetSupllierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IMapper _mapper;
        public GetSupllierService(ISupplierRepository supplierRepository, IMapper mapper)
        {
            _supplierRepository = supplierRepository;
            _mapper = mapper;
        }

        public async Task<List<SupplierDTO>> GetAllSupplier(int pageNumber, int pageSize)
        {
            var listSupplier = await _supplierRepository.GetAllSuppliers(pageNumber, pageSize);

            var supplierDTOs = _mapper.Map<List<SupplierDTO>>(listSupplier);

            return supplierDTOs;
        }
    }
}
