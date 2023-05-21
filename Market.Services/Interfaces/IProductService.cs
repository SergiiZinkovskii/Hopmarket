using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;

namespace Market.Service.Interfaces
{
    public interface IProductService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();

        IBaseResponse<List<Product>> GetProducts();

        Task<IBaseResponse<ProductViewModel>> GetProduct(long id);

        Task<BaseResponse<Dictionary<long, string>>> GetProduct(string term);

        Task<IBaseResponse<Product>> Create(ProductViewModel product, byte[] imageData, byte[] imageData2, byte[] imageData3, byte[] imageData4, byte[] imageData5);

        Task<IBaseResponse<bool>> DeleteProduct(long id);

        Task<IBaseResponse<Product>> Edit(ProductViewModel model, long Id);
    }
}