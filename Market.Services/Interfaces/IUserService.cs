using System.Collections.Generic;
using System.Threading.Tasks;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.User;

namespace Market.Service.Interfaces
{
    public interface IUserService
    {
        Task<IBaseResponse<User>> Create(UserViewModel model);
        BaseResponse<Dictionary<int, string>> GetRoles();
        Task<BaseResponse<IEnumerable<UserViewModel>>> GetUsers();
        Task<IBaseResponse<bool>> DeleteUser(long id);
    }
}