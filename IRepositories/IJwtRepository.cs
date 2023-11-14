using JwtAuthExample.Models;

namespace JwtAuthExample.IRepositories
{
    public interface IJwtRepository
    {
        string GenerateJwtToken(User user);
    }
}
