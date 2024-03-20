using Domain.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.GetProductService.Interface
{
    public interface IGetProductService
    {
        public Task<List<ProductDTO>> GetAllProduct(int pageNumber, int pageSize);
        public Task<List<ProductSupplierDTO>> GetProductByCode(string codigo);
    }
}
