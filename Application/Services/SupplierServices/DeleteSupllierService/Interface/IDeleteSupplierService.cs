using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.DeleteSupllierService.Interface
{
    public interface IDeleteSupplierService
    {
        public Task<bool> DeleteSupplier(int id);
    }
}
