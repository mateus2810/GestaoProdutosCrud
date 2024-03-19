using Domain.DTO;
using System.Threading.Tasks;

namespace Application.Services.ProductServices.GetProductService.Interface
{
    public interface IGetProductService
    {
        public Task<ProductDTO> GetAllProduct();
    }
}
