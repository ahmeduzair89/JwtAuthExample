using JwtAuthExample.IRepositories;
using JwtAuthExample.WrapperModels.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtAuthExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepo;
       public UserController(IUserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

       [HttpPost("LoginUser")]
       public async Task<IActionResult> LoginUser([FromBody] UserLoginModel model)
        {
            var res = await userRepo.LoginUser(model);
            return res.Success ? Ok(res) : BadRequest();    
        }



        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterModel model)
        {
            var res = await userRepo.RegisterUser(model);
            return Ok(res);
        }

        [HttpGet("CheckAuthenticated")]
        [Authorize]
        public async Task<IActionResult> CheckAuthenticated()
        {
         ClaimsPrincipal currentUser =    HttpContext.User;
            var res = await userRepo.CheckAuthenticated(currentUser);
            return Ok(res);
        }
    }
}
