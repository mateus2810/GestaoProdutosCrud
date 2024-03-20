using Domain.Input;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISupplierRepository
    {
        public Task<List<SupplierModel>> GetAllSuppliers();
        public Task<bool> CreateSupplier(SupplierInput supplierInput);
    }
}
