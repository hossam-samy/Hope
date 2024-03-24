using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using Hope.Core.Features.PostOperation.Commands.CreatePostForPeople;
using Hope.Core.Features.PostOperation.Commands.CreatePostForThings;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfThings;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfShelters;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfLosties;
using Hope.Core.Features.CommentOperation.Queries.GetReplies;
using Hope.Core.Features.PostOperation.Commands.DeletePost;
using Hope.Core.Features.PostOperation.Commands.UpdatePostOfPeople;
using Hope.Core.Features.PostOperation.Commands.UpdatePostOfThings;
using Hope.Core.Features.PostOperation.Commands.HidePosts;
using Hope.Core.Features.PostOperation.Commands.PinPost;
using Hope.Core.Features.PostOperation.Commands.UnPinPost;
using Hope.Core.Features.CommentOperation.Commands.AddCommentToPost;
using Hope.Core.Features.CommentOperation.Commands.AddCommentToComment;
using Hope.Core.Features.CommentOperation.Commands.UpdateComment;
using Hope.Core.Features.CommentOperation.Commands.DeleteComment;
using Hope.Core.Features.CommentOperation.Queries.GetCommentsByPostId;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async  Task<IActionResult> AddPostPeople ([FromForm]CreatePostForPeopleCommand  command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> AddPostThings([FromForm] CreatePostForThingsCommand command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostThings(int PageNumber)
        {
            
            return Ok(await _mediator.Send(new GetAllPostsOfThingsQuery { PageNumber = PageNumber, UserId= User.Claims.Where(i => i.Type == "uid").First().Value }));

        }
        [HttpGet]
       
        public async Task<IActionResult> GetAllPosts(int PageNumber)
        {
            return Ok(await _mediator.Send(new GetAllPostsQuery { PageNumber = PageNumber,UserId = User.Claims.Where(i => i.Type == "uid").First().Value}));
            
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfShelters(int PageNumber)
        {
            return Ok(await _mediator.Send(new GetAllPostsOfSheltersQuery { PageNumber = PageNumber, UserId = User.Claims.Where(i => i.Type == "uid").First().Value}));
           
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfAccidents(int PageNumber)
        {
          

            return Ok(await _mediator.Send(new GetAllPostsOfAccidentsQuery { PageNumber = PageNumber, UserId = User.Claims.Where(i => i.Type == "uid").First().Value}));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfLosties(int PageNumber)
        {
           
            return Ok(await _mediator.Send(new GetAllPostsOfLostiesQuery { PageNumber = PageNumber, UserId = User.Claims.Where(i => i.Type == "uid").First().Value}));
        }

        [HttpGet]
        public async Task<IActionResult> GetReplies(int id)
        {
            
            return Ok(await _mediator.Send(new GetRepliesQuery {Id=id }));
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePost(DeletePostCommand  command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;
            return Ok(await _mediator.Send(command));
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePostOfThingsPost(UpdatePostOfThingsCommand command)
        {

            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
          
        [HttpPut]
        public async Task<IActionResult> UpdatePostOfPeoplePost(UpdatePostOfPeopleCommand command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> HidePost(HidePostsCommand command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> PinPost(PinPostCommand command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> UnPinPost(UnPinPostCommand command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(AddCommentToPostCommand  command)
        {
            command.UserId=User.Claims.Where(i=>i.Type== "uid").First().Value;  
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> AddCommentToComment(AddCommentToCommentCommand command)
        {

            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentCommand  command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(DeleteCommentCommand command)
        {
            command.UserId = User.Claims.Where(i => i.Type == "uid").First().Value;

            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult>GetCommentsByPostId (int PostId,bool IsPeople )
        {
                    
            return Ok(await _mediator.Send(new GetCommentsByPostIdQuery() { PostId=PostId,IsPeople=IsPeople}));
        }





        //[HttpDelete]
        //public async Task<IActionResult> DeleteFilePost(string url)
        //{
        //    return Ok(await _postService.DeleteFileAsync(url));
        //}

    }
}
