using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IMailService mailService;
        private readonly IMapper mapper;

        public AccountController(IAuthService authService, IMailService mailService, IMapper mapper)
        {
            this.authService = authService;
            this.mailService = mailService;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister(UserDto dto) { 
             
             return Ok(await authService.UserRegister(dto)); 
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

            return Ok(await authService.Login(dto));
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEmail( MailRequestDto dto)
        {
            await mailService.SendEmailAsync(dto.ToEmail, dto.Subject, dto.Body);

            return Ok("good");

        }


    }
}
