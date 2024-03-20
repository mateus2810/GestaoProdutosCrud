using Application.Services.SupplierServices.UpdateSupllierService.Interface;
using Domain.Input;
using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.UpdateSupllierService
{
    public class UpdateSupllierService : IUpdateSupllierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public UpdateSupllierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<bool> UpdateSupplier(int id, SupplierInput supplierInput)
        {
            var update = await _supplierRepository.UpdateSupplier(id, supplierInput);
            return update;
        }
    }
}
