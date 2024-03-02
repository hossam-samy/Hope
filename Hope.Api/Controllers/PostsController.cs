﻿using Hope.Core.Interfaces;
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
        public async Task<IActionResult> GetPostThings()
        {
              
            return Ok(await _postService.GetPostThings());
        }
        [HttpGet]
       
        public async Task<IActionResult> GetAllPosts()
        {
            return Ok(await _postService.GetAllPosts());
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfShelters()
        {
            return Ok(await _postService.GetPostOfShelters());
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfAccidents()
        {
          
            return Ok(await _postService.GetPostOfAccidents());
        }
        [HttpGet]
        public async Task<IActionResult> GetPostOfLosties()
        {
            return Ok(await _postService.GetPostOfLosties());
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePost(int id,bool IsPeople)
        {
            return Ok(await _postService.DeletePost(id, IsPeople));
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
        public async Task<IActionResult> HidePost(string UserId, int PostId, bool IsPeople)
        {
            var result=IsPeople? await _postService.HidePost<PostOfLostPeople>(UserId, PostId):
                 await _postService.HidePost<PostOfLostThings>(UserId, PostId);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> PinPost(string UserId, int PostId, bool IsPeople)
        {
            var result = IsPeople ? await _postService.PinPost<PostOfLostPeople>(UserId, PostId) :
                await _postService.PinPost<PostOfLostThings>(UserId, PostId);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UnPinPost(string UserId, int PostId, bool IsPeople)
        {
            var result = IsPeople ? await _postService.UnPinPost<PostOfLostPeople>(UserId, PostId) :
                await _postService.UnPinPost<PostOfLostThings>(UserId, PostId);

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
        public async Task<IActionResult> DeleteComment(int id)
        {
            return Ok(await _postService.DeleteComment(id));
        }




        [HttpDelete]
        public async Task<IActionResult> DeleteFilePost(string url)
        {
            return Ok(await _postService.DeleteFileAsync(url));
        }

    }
}
