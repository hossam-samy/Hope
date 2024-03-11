using Hope.Core.Interfaces;
using Hope.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Hope.Domain.Model;
using System.Text.Json;


namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
   // [Authorize(Roles ="User")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService; 
        public PostsController( IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async  Task<IActionResult> AddPostPeople ([FromForm]PostPeopleRequest dto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            
            

            return Ok(await _postService.AddPostPeople(dto));
        }
        [HttpPost]
        public async Task<IActionResult> AddPostThings([FromForm]PostThingsRequest dto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            
             

            return Ok(await _postService.AddPostThings(dto));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostThings(int? cursor, string UserId)
        {
              
            return Ok(await _postService.GetPostThings(cursor,UserId));
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAllPosts(int? Peoplecursor, int?  thingcursor, string UserId)
        {
            return Ok(await _postService.GetAllPosts(Peoplecursor,thingcursor,UserId));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfShelters(int? cursor, string UserId)
        {
            return Ok(await _postService.GetPostOfShelters(cursor, UserId));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfAccidents(int? cursor, string UserId)
        {
          
            return Ok(await _postService.GetPostOfAccidents(cursor, UserId));
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfLosties(int? cursor, string UserId)
        {
            return Ok(await _postService.GetPostOfLosties(cursor, UserId));
        }

        [HttpGet]
        public async Task<IActionResult> GetReplies(int id)
        {
            return Ok(await _postService.GetReplies(id));
        }


        [HttpDelete]
        public async Task<IActionResult> DeletePost(DeletePostRequests requests)
        {
            return Ok(await _postService.DeletePost(requests));
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePostOfThingsPost(UpdatePostOfThingsRequest request)
        {
            return Ok(await _postService.UpdatePostOfThingsPost(request));
        }
        [HttpPut]
        public async Task<IActionResult> UpdatePostOfPeoplePost(UpdatePostOfPeopleRequest request)
        {
            return Ok(await _postService.UpdatePostOfPeoplePost(request));
        }
        [HttpPost]
        public async Task<IActionResult> HidePost(ServiceRequests requests)
        {
            var result= requests.IsPeople ? await _postService.HidePost<PostOfLostPeople>(requests.UserId, requests.PostId) :
                 await _postService.HidePost<PostOfLostThings>(requests.UserId, requests.PostId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PinPost(ServiceRequests requests)
        {
            var result = requests.IsPeople ? await _postService.PinPost<PostOfLostPeople>(requests.UserId, requests.PostId) :
                await _postService.PinPost<PostOfLostThings>(requests.UserId, requests.PostId);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UnPinPost(ServiceRequests requests)
        {
            var result = requests.IsPeople ? await _postService.UnPinPost<PostOfLostPeople>(requests.UserId, requests.PostId) :
                await _postService.UnPinPost<PostOfLostThings>(requests.UserId, requests.PostId);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCommentToPost(CommentRequest request)
        {
            var result = request.IsPeople ? await _postService.AddCommentToPost<PostOfLostPeople> (request) :
                await _postService.AddCommentToPost<PostOfLostThings> (request);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCommentToComment(AddingCommentToCommentRequest request)
        {
            return Ok(await _postService.AddCommentToComment(request));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateComment(UpdateCommentRequest request)
        {
            return Ok(await _postService.UpdateComment(request));
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteComment(DeleteCommentRequests requests)
        {
            return Ok(await _postService.DeleteComment(requests));
        }





        //[HttpDelete]
        //public async Task<IActionResult> DeleteFilePost(string url)
        //{
        //    return Ok(await _postService.DeleteFileAsync(url));
        //}

    }
}
