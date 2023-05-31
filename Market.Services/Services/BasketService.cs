using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.Extensions;
using Market.Domain.Response;
using Market.Domain.ViewModels.Order;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Market.Service.Implementations;

public class BasketService : IBasketService
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IBaseRepository<Photo> _photoRepository;
    private readonly IBaseRepository<Order> _orderRepository;

    public BasketService(IBaseRepository<User> userRepository, IBaseRepository<Product> productRepository, IBaseRepository<Photo> photoRepository, IBaseRepository<Order> orderRepository)
    {
        _userRepository = userRepository;
        _productRepository = productRepository;
        _photoRepository = photoRepository;
        _orderRepository = orderRepository;
    }

    public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetAllItems()
    {
        try
        {
            var orders = await _orderRepository.GetAll().ToListAsync();

            var response = from order in orders
                           join product in _productRepository.GetAll() on order.ProductId equals product.Id

                           select new OrderViewModel()
                           {
                               Id = order.Id,
                               ProductName = product.Name,
                               Power = product.Power,
                               TypeProduct = product.TypeProduct.GetDisplayName(),
                               Model = product.Model,
                               
                           };

            return new BaseResponse<IEnumerable<OrderViewModel>>()
            {
                Data = response,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<OrderViewModel>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }

    }

    public async Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName)
    {
        try
        {


            var user = await _userRepository.GetAll()
                
                .Include(x => x.Basket)
                .ThenInclude(x => x.Orders)
                .FirstOrDefaultAsync(x => x.Name == userName);

                if (user == null)
            {
                return new BaseResponse<IEnumerable<OrderViewModel>>()
                {
                    Description = "Користувача не знайдено",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var orders = user.Basket?.Orders;
            var response = from p in orders
                           join c in _productRepository.GetAll() on p.ProductId equals c.Id
                           join photo in _photoRepository.GetAll() on c.Id equals photo.ProductId into photos
                           from photo in photos.DefaultIfEmpty()
                           select new OrderViewModel()
                           {
                               Id = p.Id,
                               ProductName = c.Name,
                               Power = c.Power,
                               TypeProduct = c.TypeProduct.GetDisplayName(),
                               Model = c.Model,
                               Photo = photo,
                           };

            return new BaseResponse<IEnumerable<OrderViewModel>>()
            {
                Data = response,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<IEnumerable<OrderViewModel>>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }


    public async Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id)
    {
        try
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Basket)
                .ThenInclude(x => x.Orders)
                .FirstOrDefaultAsync(x => x.Name == userName);

            if (user == null)
            {
                return new BaseResponse<OrderViewModel>()
                {
                    Description = "Користувача не знайдено",
                    StatusCode = StatusCode.UserNotFound
                };
            }

            var orders = user.Basket?.Orders.Where(x => x.Id == id).ToList();
            if (orders == null || orders.Count == 0)
            {
                return new BaseResponse<OrderViewModel>()
                {
                    Description = "Замовлень немає",
                    StatusCode = StatusCode.OrderNotFound
                };
            }

            var response = (from p in orders
                            join c in _productRepository.GetAll() on p.ProductId equals c.Id
                            join photo in _photoRepository.GetAll() on c.Id equals photo.ProductId into photos
                            from photo in photos.DefaultIfEmpty()
                            select new OrderViewModel()
                            {
                                Id = p.Id,
                                ProductName = c.Name,
                                Power = c.Power,
                                TypeProduct = c.TypeProduct.GetDisplayName(),
                                Model = c.Model,
                                Address = p.Address,
                                FirstName = p.FirstName,
                                LastName = p.LastName,
                                MiddleName = p.MiddleName,
                                DateCreate = p.DateCreated.ToLongDateString(),
                                Photo = photo,
                            }).FirstOrDefault();

            return new BaseResponse<OrderViewModel>()
            {
                Data = response,
                StatusCode = StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<OrderViewModel>()
            {
                Description = ex.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}