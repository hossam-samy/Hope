using Hope.Core.Features.AdminOperation.Commands.DeletePost;
using Hope.Core.Features.AdminOperation.Commands.DeleteUser;
using Hope.Core.Features.AdminOperation.Queries.GetAllUsers;
using Hope.Core.Features.AdminOperation.Queries.GetCountOfCreatedPosts;
using Hope.Core.Features.AdminOperation.Queries.GetCountOfCreatedUsers;
using Hope.Core.Features.AdminOperation.Queries.GetNumberOfNewAccountsForWeek;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpDelete]
        public async Task<IActionResult> DeletePost(AdminDeletePostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUser(AdminDeleteUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetUserCount( )
        {
            return Ok(await _mediator.Send(new AdminGetCountOfCreatedUsersQuery() ));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostsCount()
        {
            return Ok(await _mediator.Send(new AdminGetCountOfCreatedPostsQuery ()));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers( )
        {
            return Ok(await _mediator.Send(new AdminGetAllUsersQuery()));
        }
        [HttpGet]
        public async Task<IActionResult> GetNumberOfNewUserPerWeek()
        {
            return Ok(await _mediator.Send(new GetNumberOfNewAccountsForWeekQuery()));
        }

    }
}
