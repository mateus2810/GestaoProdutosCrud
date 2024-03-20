using Domain.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.UpdateProductService.Interface
{
    public interface IUpdateProductService
    {
        public Task<bool> UpdateProduct(int id, ProductInput productInput);
    }
}
