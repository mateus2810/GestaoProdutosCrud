using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SupplierServices.GetSupllierService.Interface
{
    public interface IGetSupllierService
    {
        public Task<List<SupplierDTO>> GetAllSupplier();

    }
}
