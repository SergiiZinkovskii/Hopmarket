using Market.DAL.Interfaces;
using Market.DAL.Repositories;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Extensions;
using Market.Domain.Response;
using Market.Domain.ViewModels.Product;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICommentRepository _commentRepository;
        

        public ProductService(IProductRepository productRepository, ICommentRepository commentRepository)
        {
            _productRepository = productRepository;
            _commentRepository = commentRepository;
        }


        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((TypeProduct[])Enum.GetValues(
		                typeof(TypeProduct)))
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

        public async Task<ProductViewModel?> GetProductAsync(long id,
	        CancellationToken cancellationToken)
        {
	        var product = await _productRepository.Find(id, cancellationToken);

		        if (product == null)
		        {
			        return null;
		        }

		        return new ProductViewModel()
		        {
			        Id = product.Id,
			        DateCreate = product.DateCreate.ToLongDateString(),
			        Description = product.Description,
			        Name = product.Name,
			        Price = product.Price,
			        TypeProduct = product.TypeProduct.GetDisplayName(),
			        Power = product.Power,
			        ProdModel = product.Model,
                    Comments = await _commentRepository.FindAsync(product.Id),
                    Photos = product.Photos.Select(p => p.ImageData).ToList() // Список фото товару
		        };
        }

        public async Task<BaseResponse<Dictionary<long, string>>> GetProductAsync(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<long, string>>();
            try
            {
                var products = await _productRepository.GetAll()
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        Power = x.Power,
                        Name = x.Name,
                        Description = x.Description,
                        ProdModel = x.Model,
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

        public async Task<IBaseResponse<Product>> Create(ProductViewModel model, List<byte[]> imageDataList)
        {
            try
            {
                var product = new Product()
                {
                    Name = model.Name,
                    Model = model.ProdModel,
                    Description = model.Description,
                    DateCreate = DateTime.Now,
                    Power = model.Power,
                    TypeProduct = Enum.Parse<TypeProduct>(model.TypeProduct),
                    Price = model.Price,
                    Photos = new List<Photo>()
                };

                foreach (var imageData in imageDataList)
                {
                    var photo = new Photo()
                    {
                        ImageData = imageData
                    };
                    product.Photos.Add(photo);
                }

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
                    Description = $"[CreateAsync] : {ex.Message}",
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

        public async Task<IBaseResponse<Product>> Edit(ProductViewModel model, long Id)
        {
            try
            {
                var product = await _productRepository.GetAll().FirstOrDefaultAsync(x => x.Id == Id);
                if (product == null)
                {
                    return new BaseResponse<Product>()
                    {
                        Description = "Product not found",
                        StatusCode = StatusCode.ProductNotFound
                    };
                }

                product.Description = model.Description;
                product.Model = model.ProdModel;
                product.Price = model.Price;
                product.Power = model.Power; 
                product.DateCreate = DateTime.Now;
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
                // ToDo Move this to repository level
                var products = _productRepository.GetAll()
                    .Include(p => p.Photos) 
                    .ToList();

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