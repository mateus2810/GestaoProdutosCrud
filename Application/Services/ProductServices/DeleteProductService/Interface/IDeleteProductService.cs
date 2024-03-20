using System.Threading.Tasks;

namespace Application.Services.ProductServices.DeleteProductService.Interface
{
    public interface IDeleteProductService
    {
        public Task<bool> DeleteProduct(int id);
    }
}
