using Domain.Input;
using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISupplierRepository
    {
        public Task<List<SupplierModel>> GetAllSuppliers();
        public Task<bool> CreateSupplier(SupplierInput supplierInput);
        public Task<bool> UpdateSupplier(int id, SupplierInput supplierInput);
    }
}
