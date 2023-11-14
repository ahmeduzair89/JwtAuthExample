using JwtAuthExample.WrapperModels;
using JwtAuthExample.WrapperModels.UserModels;
using System.Security.Claims;

namespace JwtAuthExample.IRepositories
{
    public interface IUserRepository
    {
        Task<ApiWrapper> LoginUser(UserLoginModel model);
        Task<ApiWrapper> RegisterUser(UserRegisterModel model);
        Task<ApiWrapper> CheckAuthenticated(ClaimsPrincipal currentUser);
    }
}
