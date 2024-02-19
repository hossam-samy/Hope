using Hope.Core.Interfaces;
using Hope.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Hope.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles ="User")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService; 
        public PostsController( IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async  Task<IActionResult> AddPostPeople (PostPeopleDto dto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
              
            return Ok(await _postService.AddPostPeople(dto));
        }
        [HttpPost]
        public async Task<IActionResult> AddPostThings(PostThingsDto dto)
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
        public async Task<IActionResult> GetPostsOfAllPeople()
        {


            return Ok(await _postService.GetPostsOfAllPeople());
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
    }
}
