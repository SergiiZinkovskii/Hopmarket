using Market.Domain.Response;
using Market.Domain.ViewModels.Order;

namespace Market.Service.Interfaces
{
    public interface IBasketService
    {
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetItems(string userName);
        Task<IBaseResponse<IEnumerable<OrderViewModel>>> GetAllItems();

        Task<IBaseResponse<OrderViewModel>> GetItem(string userName, long id);
    }
}