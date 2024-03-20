using Domain.Input;
using Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> GetAllProduct(int pageNumber, int pageSize);
        public Task<bool> CreateProduct(ProductInput product);
        public Task<bool> UpdateProduct(int id, ProductInput product);
        public Task<bool> DeleteProduct(int id);
        public Task<List<ProductSupplierModel>> GetProductByCode(string codigo);
    }
}
