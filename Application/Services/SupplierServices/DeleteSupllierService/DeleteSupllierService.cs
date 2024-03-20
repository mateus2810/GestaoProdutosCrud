using Application.Services.SupplierServices.DeleteSupllierService.Interface;
using Domain.Interfaces;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.DeleteSupllierService
{
    public class DeleteSupllierService : IDeleteSupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        public DeleteSupllierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public Task<bool> DeleteSupplier(int id)
        {
            var delete = _supplierRepository.DeleteSupplier(id);
            return delete;
        }
    }
}
