using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Extensions;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Service.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _productRepository;

        public ProductService(IBaseRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((TypeProduct[])Enum.GetValues(typeof(TypeProduct)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ProductViewModel>> GetProduct(long id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<ProductViewModel>()
                    {
                        Description = "Товар не знайдено",
                        StatusCode = StatusCode.UserNotFound
                    };
                }

                var data = new ProductViewModel()
                {
                    DateCreate = product.DateCreate.ToLongDateString(),
                    Description = product.Description,
                    Name = product.Name,
                    Price = product.Price,
                    TypeProduct = product.TypeProduct.GetDisplayName(),
                    Speed = product.Speed,
                    Model = product.Model,
                    Image = product.Avatar,
                };

                return new BaseResponse<ProductViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductViewModel>()
                {
                    Description = $"[GetProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<long, string>>> GetProduct(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var products = await _productRepository.GetAll()
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        Speed = x.Speed,
                        Name = x.Name,
                        Description = x.Description,
                        Model = x.Model,
                        DateCreate = x.DateCreate.ToLongDateString(),
                        Price = x.Price,
                        TypeProduct = x.TypeProduct.GetDisplayName()
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = products;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<long, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> Create(ProductViewModel model, byte[] imageData)
        {
            try
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Model = model.Model,
                    Description = model.Description,
                    DateCreate = DateTime.Now,
                    Speed = model.Speed,
                    TypeProduct = (TypeProduct)Convert.ToInt32(model.TypeProduct),
                    Price = model.Price,
                    Avatar = imageData
                };
                await _productRepository.Create(product);

                return new BaseResponse<Product>()
                {
                    StatusCode = StatusCode.OK,
                    Data = product
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteProduct(long id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }

                await _productRepository.Delete(product);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteProduct] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Product>> Edit(long id, ProductViewModel model)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (product == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Product not found",
                        StatusCode = StatusCode.CarNotFound
                    };
                }

                product.Description = model.Description;
                product.Model = model.Model;
                product.Price = model.Price;
                product.Speed = model.Speed;
                product.DateCreate = DateTime.ParseExact(model.DateCreate, "yyyyMMdd HH:mm", null);
                product.Name = model.Name;

                await _productRepository.Update(product);


                return new BaseResponse<Product>()
                {
                    Data = product,
                    StatusCode = StatusCode.OK,
                };
                // TypeProduct
            }
            catch (Exception ex)
            {
                return new BaseResponse<Product>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Product>> GetProducts()
        {
            try
            {
                var products = _productRepository.GetAll().ToList();
                if (!products.Any())
                {
                    return new BaseResponse<List<Product>>()
                    {
                        Description = "Знайдено 0 елементів",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Product>>()
                {
                    Data = products,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Product>>()
                {
                    Description = $"[GetProducts] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}