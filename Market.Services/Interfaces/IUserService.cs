using System.Collections.Generic;
using System.Threading.Tasks;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.User;

namespace Market.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> CreateAsync(UserViewModel model);
        BaseResponse<Dictionary<int, string>> GetRoles();
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsersAsync();
        Task<IBaseResponse<bool>> DeleteUserAsync(long id);
    }
}