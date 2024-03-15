﻿using FluentValidation;
using Hope.Core.Features.Authentication.Commands.AddUserImage;
using Hope.Core.Features.Authentication.Commands.ChangePassword;
using Hope.Core.Features.Authentication.Commands.Register;
using Hope.Core.Features.Authentication.Queries.GetAllUsers;
using Hope.Core.Features.Authentication.Queries.GetProfile;
using Hope.Core.Features.Authentication.Queries.Login;
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
        public async Task<IActionResult> UserRegister(RegisterCommand command) { 
            
             return Ok(await _mediator.Send(command)); 
            
         }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddUserImage(AddUserImageCommand  command)
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
        public async Task<IActionResult> GetAllUsers()
        {

            return Ok(await _mediator.Send(new GetAllUsersQuery()));

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
        public async Task<IActionResult> Login( LoginQuery query)
        {

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> SendVerfingEmailEmail( string userEmail )
        {
            await _mailService.SendEmailAsync(userEmail);

            return Ok("good");

        }
        [HttpPost]
        public async Task<IActionResult> SendEmailForChangePasswordAsync(string userEmail)
        {
            return Ok(await _mailService.SendEmailForChangePasswordAsync(userEmail));


        }
        [HttpGet]
        public async Task<IActionResult> GetConfirmationNumber(string num)
        {
                 return Ok( await _mailService.GetConfirmationNumber(num));
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _mediator.Send(command);    

            return Ok("good");

        }



    }
}
