using FluentValidation;
using Hope.Core.Dtos;
using Hope.Core.Features.Authentication.Commands.AddUserImage;
using Hope.Core.Features.Authentication.Commands.ChangePassword;
using Hope.Core.Features.Authentication.Commands.Register;
using Hope.Core.Features.Authentication.Commands.UpdateUserData;
using Hope.Core.Features.Authentication.Queries.GetAllUsers;
using Hope.Core.Features.Authentication.Queries.GetProfile;
using Hope.Core.Features.Authentication.Queries.Login;
using Hope.Core.Features.PostOperation.Queries.GetPinnedPostsByUserId;
using Hope.Core.Features.PostOperation.Queries.GetPostsByUserId;
using Hope.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;   
        private readonly IMailService _mailService;

        public AccountController(IMediator mediator, IMailService mailService)
        {

            _mediator = mediator;
            _mailService = mailService;
        }
        [HttpPost]
        public async Task<IActionResult> UserRegister([FromForm]RegisterCommand command) { 
            
             return Ok(await _mediator.Send(command)); 
            
         }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddUserImage([FromForm]AddUserImageCommand  command)
        {

            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));


        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetProfile()
        {



            return Ok(await _mediator.Send(new GetProfileQuery() {UserId= User.Claims.Where(i => i.Type == "uid").First().Value })) ;

        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPostsByUserId()
        {

            return Ok(await _mediator.Send(new GetPostsByUserIdQuery() { UserId = User.Claims.Where(i => i.Type == "uid").First().Value }));

        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPinnedPostsByUserId()
        {

            return Ok(await _mediator.Send(new GetPinnedPostsByUserIdQuery() { UserId = User.Claims.Where(i => i.Type == "uid").First().Value }));

        }
        [HttpPut]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateUserData(UpdateUserDataCommand command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;
            return Ok(await _mediator.Send(command));

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {

            return Ok(await _mediator.Send(new GetAllUsersQuery()));

        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginQuery query)
        {

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> SendVerfingEmailCode(SendVerfingEmailCodeRequest request )
        {
            return Ok( await _mailService.SendEmailAsync(request.userEmail));

            
        }
        [HttpPost]
        public async Task<IActionResult> SendCodeForChangePasswordAsync(SendEmailForChangePasswordRequest request)
        {
            return Ok(await _mailService.SendEmailForChangePasswordAsync(request.userEmail));


        }
        [HttpPost]
        public async Task<IActionResult> GetConfirmationNumber(GetConfirmationNumberRequest request)
        {
                 return Ok( await _mailService.GetConfirmationNumber(request.UserEmail,request.num));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
           return Ok(await _mediator.Send(command));    
        }



    }
}
