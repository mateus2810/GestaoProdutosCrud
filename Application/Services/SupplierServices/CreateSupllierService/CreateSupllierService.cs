using Application.Services.SupplierServices.CreateSupllierService.Interface;
using Domain.Input;
using Domain.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.CreateSupllierService
{
    public class CreateSupllierService : ICreateSupllierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public CreateSupllierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;

        }
        public Task<bool> CreateSupplier(SupplierInput supplierInput)
        {
            var supplier = _supplierRepository.CreateSupplier(supplierInput);

            return supplier;
        }
    }
}
