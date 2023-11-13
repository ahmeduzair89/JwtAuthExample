using JwtAuthExample.IRepositories;
using JwtAuthExample.Models;
using JwtAuthExample.WrapperModels;
using JwtAuthExample.WrapperModels.UserModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;

namespace JwtAuthExample.DataRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestDbContext db;
        private readonly IConfiguration configuration;

        public UserRepository(TestDbContext db, IConfiguration configuration) {
            this.db = db;
            this.configuration = configuration;
        }


        public Task<ApiWrapper> LoginUser(UserLoginModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ApiWrapper> RegisterUser(UserRegisterModel model)
        {
            try
            {
                User localUser = new User();
                localUser.Name = model.Name;
                localUser.Email = model.Email;
                localUser.ProfilePicture = model.ProfilePicture ?? "https://i.pravatar.cc/300";
                localUser.Password = GeneratePasswordHash(model.Password);
                db.Users.Add(localUser);
                db.SaveChanges();

         /// implement here
            }
            catch (Exception e)
            {

                throw;
            }
            
        }

        private string GeneratePasswordHash(string password)
        {
            string hashedPassword = string.Empty;

            byte[] salt = System.Text.Encoding.UTF8.GetBytes(configuration.GetValue<String>("PasswordHashKey"));
            hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password!,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10,
            numBytesRequested: 256 / 8));
            return hashedPassword;
        }
    }
}

