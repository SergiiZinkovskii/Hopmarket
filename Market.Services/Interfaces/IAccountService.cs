using System.Security.Claims;
using System.Threading.Tasks;
using Market.Domain.Response;
using Market.Domain.ViewModels.Account;

namespace Market.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Register(RegisterViewModel model);
        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);
        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);
    }
}