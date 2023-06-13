using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Order;

namespace Market.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> Create(CreateOrderViewModel model);
        Task<IBaseResponse<bool>> Delete(long id);
    }
}