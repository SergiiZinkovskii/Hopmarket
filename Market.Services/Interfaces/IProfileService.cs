using System.Collections.Generic;
using System.Threading.Tasks;
using Market.Domain.Entity;
using Market.Domain.Response;
using Market.Domain.ViewModels.Profile;
using Market.Domain.ViewModels.User;

namespace Market.Service.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<ProfileViewModel>> GetProfile(string userName);
        Task<BaseResponse<Profile>> Save(ProfileViewModel model);
    }
}