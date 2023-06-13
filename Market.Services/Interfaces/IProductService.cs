using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;

namespace Market.Service.Interfaces
{
    public interface IProductService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();
        IBaseResponse<List<Product>> GetProducts();
        Task<ProductViewModel?> GetProductAsync(long id,
	        CancellationToken cancellationToken);
        Task<BaseResponse<Dictionary<long, string>>> GetProductAsync(string term);
        Task<IBaseResponse<Product>> Create(ProductViewModel model, List<byte[]> imageDataList);
        Task<IBaseResponse<bool>> DeleteProduct(long id);
        Task<IBaseResponse<Product>> Edit(ProductViewModel model, long Id);
    }
}