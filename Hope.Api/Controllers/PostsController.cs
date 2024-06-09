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
using Hope.Core.Features.PostOperation.Queries.GetArchivedPosts;
using Hope.Core.Features.PostOperation.Commands.UnHidePost;
using Hope.Core.Features.PostOperation.Queries.GetPostByPostId;
using Hope.Core.Features.PostOperation.Queries.GetRecommendedPosts;

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

            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> AddPostThings( [FromForm]CreatePostForThingsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostThings(int PageNumber)
        {
            
            return Ok(await _mediator.Send(new GetAllPostsOfThingsQuery { PageNumber = PageNumber }));

        }
        [HttpGet]
       
        public async Task<IActionResult> GetAllPosts(int PageNumber)
        {
            
            return Ok(await _mediator.Send(new GetAllPostsQuery { PageNumber = PageNumber }));
            
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfShelters(int PageNumber)
        {
            return Ok(await _mediator.Send(new GetAllPostsOfSheltersQuery { PageNumber = PageNumber }));
           
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfAccidents(int PageNumber)
        {
          

            return Ok(await _mediator.Send(new GetAllPostsOfAccidentsQuery { PageNumber = PageNumber }));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfLosties(int PageNumber)
        {
           
            return Ok(await _mediator.Send(new GetAllPostsOfLostiesQuery { PageNumber = PageNumber}));
        }

        [HttpGet]
        public async Task<IActionResult> GetReplies(int id)
        {
            
            return Ok(await _mediator.Send(new GetRepliesQuery {Id=id }));
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePost(DeletePostCommand  command)
        {
            return Ok(await _mediator.Send(command));
            
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePostOfThingsPost(UpdatePostOfThingsCommand command)
        {

            return Ok(await _mediator.Send(command));
        }
          
        [HttpPut]
        public async Task<IActionResult> UpdatePostOfPeoplePost(UpdatePostOfPeopleCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> HidePost(HidePostsCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> PinPost(PinPostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> UnPinPost(UnPinPostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(AddCommentToPostCommand  command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost]
        public async Task<IActionResult> AddCommentToComment(AddCommentToCommentCommand command)
        {

            return Ok(await _mediator.Send(command));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentCommand  command)
        {

            return Ok(await _mediator.Send(command));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(DeleteCommentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult>GetCommentsByPostId (int PostId,bool IsPeople )
        {
                    
            return Ok(await _mediator.Send(new GetCommentsByPostIdQuery() { PostId=PostId,IsPeople=IsPeople}));
        }
        [HttpGet]
        public async Task<IActionResult> GetArchivedPosts()
        {

            return Ok(await _mediator.Send(new GetArchivedPostsQuery()));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostByPostId(int postId,bool IsPeople)
        {

            return Ok(await _mediator.Send(new GetPostByPostIdQuery() {PostId=postId,IsPeople=IsPeople }));
        }
        [HttpGet]
        public async Task<IActionResult> GetRecommendedPosts(double Longitude, double Latitude)
        {

            return Ok(await _mediator.Send(new GetRecommendedPostsQuery() { Latitude=Latitude, Longitude=Longitude}));
        }
        [HttpPost]
        public async Task<IActionResult> UnHidePosts(UnHidePostCommand command)
        {
            return Ok(await _mediator.Send(command));
        }





        //[HttpDelete]
        //public async Task<IActionResult> DeleteFilePost(string url)
        //{
        //    return Ok(await _postService.DeleteFileAsync(url));
        //}

    }
}
