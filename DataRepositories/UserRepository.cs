using JwtAuthExample.IRepositories;
using JwtAuthExample.Models;
using JwtAuthExample.WrapperModels;
using JwtAuthExample.WrapperModels.UserModels;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace JwtAuthExample.DataRepositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TestDbContext db;
        private readonly IConfiguration configuration;
        private readonly IJwtRepository jwtRepository;

        public UserRepository(TestDbContext db, IConfiguration configuration, IJwtRepository jwtRepository) {
            this.db = db;
            this.jwtRepository = jwtRepository;
            this.configuration = configuration;
        }

        public async Task<ApiWrapper> CheckAuthenticated(ClaimsPrincipal currentUser)

        {
            try
            {
                var userInfo = new Dictionary<string, dynamic>()
{
    { "Name", currentUser?.Claims?.Where(c=>c.Type.Equals(ClaimTypes.Name)).FirstOrDefault()?.Value },
    { "Email", currentUser?.Claims?.Where(c=>c.Type.Equals(ClaimTypes.Email)).FirstOrDefault()?.Value },
    { "Id", currentUser?.Claims?.Where(c=>c.Type.Equals(ClaimTypes.NameIdentifier)).FirstOrDefault()?.Value },

};


                return ApiWrapper.SetResponse(success: true, data: userInfo, error: null);

            }
            catch (Exception e)
            {
                return ApiWrapper.SetResponse(success: false, data: e.ToString(), error: null);


            }



        }

        public async Task<ApiWrapper> LoginUser(UserLoginModel model)
        {
            try
            {
               User? user = db.Users
                    .Where(u => u.Email.Equals(model.email))
                    .Where(u => u.Password.Equals(model.password)).FirstOrDefault();

               if(user is not null)
                {
                    string jwt = jwtRepository.GenerateJwtToken(user);
                    return ApiWrapper.SetResponse(success: true, data: jwt, error: null);
                }
                else
                {
                    return ApiWrapper.SetResponse(success: true, data: null, error: "No user found with matching credentials");
                }

            }
            catch (Exception e)
            {
                return ApiWrapper.SetResponse(success: true, data: null, error: e.ToString());
            }
        }

        public async Task<ApiWrapper> RegisterUser(UserRegisterModel model)
        {
            try
            {
                User localUser = new User();
                localUser.Name = model.Name;
                localUser.Email = model.Email;
                localUser.ProfilePicture = model.ProfilePicture ?? "https://i.pravatar.cc/300";
                localUser.Contact = model.Contact;
                localUser.Password = GeneratePasswordHash(model.Password);
                db.Users.Add(localUser);
                db.SaveChanges();
                string jwtToken = jwtRepository.GenerateJwtToken(user: localUser);
                return ApiWrapper.SetResponse(success: true, data: jwtToken, error: null);
            }
            catch (Exception e)
            {
               if(e.InnerException.Message.Contains("Violation of UNIQUE KEY constraint"))
                {
                    return ApiWrapper.SetResponse(success: false, data: null, error:"User already exists");


                }
                return ApiWrapper.SetResponse(success: false, data: null, error: e.ToString());

               
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

