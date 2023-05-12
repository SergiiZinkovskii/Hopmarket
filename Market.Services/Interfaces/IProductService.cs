﻿using Market.Domain.Entity;
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

        Task<IBaseResponse<Product>> Create(ProductViewModel car, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteProduct(long id);

        Task<IBaseResponse<Product>> Edit(long id, ProductViewModel model);
    }
}