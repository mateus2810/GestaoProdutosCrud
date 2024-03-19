using Domain.Interfaces;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        public Task<ProductModel> GetAllProduct()
        {
            throw new NotImplementedException();
        }
    }
}
