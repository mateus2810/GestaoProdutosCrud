using Domain.Input;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.CreateProductService.Interface
{
    public interface ICreateProductServices
    {
        public Task<(bool Success, string ErrorMessage)> CreateProduct(ProductInput productInput);
    }
}
