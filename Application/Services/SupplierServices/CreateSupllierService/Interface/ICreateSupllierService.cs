using Domain.Input;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.CreateSupllierService.Interface
{
    public interface ICreateSupllierService
    {
        public Task<bool> CreateSupplier(SupplierInput supplierInput);
    }
}
