using Domain.Input;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.UpdateSupllierService.Interface
{
    public interface IUpdateSupllierService
    {
        public Task<bool> UpdateSupplier(int id, SupplierInput supplierInput);
    }
}
