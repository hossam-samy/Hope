using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IMailService mailService;

        public AccountController(IAuthService authService, IMailService mailService)
        {
            this.authService = authService;
            this.mailService = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(UserDto dto) { 
             
            if(!ModelState.IsValid) { return BadRequest(ModelState); }  

             return Ok(await authService.UserRegister(dto)); 
            
         }

        [HttpPost]
        public async Task<IActionResult> AddUserImage(AddImageRequest request)
        {

            return Ok(await authService.AddUserImage(request));

        }

        [HttpGet]
        public async Task<IActionResult> GetProfile(string UserId)
        {



            return Ok(await authService.GetProfile(UserId));

        }

        [HttpGet]
        public async Task<IActionResult> SearchForUsers(string name)
        {

            

            return Ok(await authService.SearchForUsers(name));

        }


        //[HttpPost]
        //public async Task<IActionResult> AdminRegister([FromForm] UserDto User)
        //{
        //    if (!ModelState.IsValid) { return BadRequest(ModelState); }
        //    var result = await authService.AdminRegister(User);
        //    if (!result.IsAuthenticated) return BadRequest(result.Message);
        //    return Ok(result);
        //}
        [HttpPost]
        //[Authorize(Roles ="User")]
        public async Task<IActionResult> Login( LoginRequest dto)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            return Ok(await authService.Login(dto));
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail( string userEmail )
        {
            await mailService.SendEmailAsync(userEmail);

            return Ok("good");

        }
        [HttpGet]
        public async Task<IActionResult> GetConfirmationNumber(string num)
        {
                 return Ok( await mailService.GetConfirmationNumber(num));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(string userEmail,string password)
        {
            await authService.ChangePassword(userEmail,password);

            return Ok("good");

        }



    }
}
