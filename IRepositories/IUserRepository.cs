using JwtAuthExample.WrapperModels;
using JwtAuthExample.WrapperModels.UserModels;

namespace JwtAuthExample.IRepositories
{
    public interface IUserRepository
    {
        Task<ApiWrapper> LoginUser(UserLoginModel model);
        Task<ApiWrapper> RegisterUser(UserRegisterModel model);
    }
}
