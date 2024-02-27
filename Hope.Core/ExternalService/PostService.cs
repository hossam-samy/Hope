using Hope.Core.Common;
using Hope.Core.Common.Consts;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Net.Http;

namespace Hope.Core.Service
{
    public class PostService:IPostService
    {
        private readonly IUnitofWork work;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<PostService> localizer;
        private readonly IMediaService mediaService;
        private readonly HttpContext httpContext;
        public PostService(IUnitofWork work, IMapper mapper, IStringLocalizer<PostService> localizer, IMediaService mediaService, IHttpContextAccessor httpContextAccessor)
        {
            this.work = work;
            this.mapper = mapper;
            this.localizer = localizer;
            this.mediaService = mediaService;
            this.httpContext = httpContextAccessor.HttpContext;
        }
        public async Task<Response> AddPostPeople([FromForm]PostPeopleRequest dto)
        {

            var extension = dto.ImageFile.FileName.Split('.')[1];

            if (dto.ImageFile!=null)
                await mediaService.AddFileAsync(dto.ImageFile,$"{dto.Name}.{extension}");

            

            var post = mapper.Map<PostOfLostPeople>(dto);
            if (dto.ImageFile != null) 
           post.ImageUrl= @$"{httpContext.Request.Scheme}://{httpContext.Request.Host}/PostsImages/" + $"{dto.Name}.{extension}";

            await work.Repository<PostOfLostPeople>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }

        public async Task<Response> AddPostThings([FromForm]PostThingsRequest dto)
        {
            var extension = dto.ImageFile.FileName.Split('.')[1];

            if (dto.ImageFile != null)
                await mediaService.AddFileAsync(dto.ImageFile, $"{dto.Type}.{extension}");


            var post = mapper.Map<PostOfLostThings>(dto);
            if (dto.ImageFile != null)
                post.ImageUrl = @$"{httpContext.Request.Scheme}://{httpContext.Request.Host}/PostsImages/" + $"{dto.Type}.{extension}";

            await work.Repository<PostOfLostThings>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);

        }

        public async Task<Response> GetPostsOfAllPeople()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i=>i);


            var output = mapper.Map<IEnumerable<PostThingResponse>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);

        }
        public async Task<Response> GetPostOfAccidents()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i=>i.Condition==Condition.accidents);


            var output = mapper.Map<IEnumerable<PostThingResponse>>(posts);



            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfLosties()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.losties);


            var output = mapper.Map<IEnumerable<PostThingResponse>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfShelters()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.shelters);


            var output = mapper.Map<IEnumerable<PostThingResponse>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }


        public async Task<Response> GetPostThings()
        {
            var posts = await work.Repository<PostOfLostThings>().Get(i => i);

            var output = mapper.Map<IEnumerable<PostThingResponse>>(posts);

            
            
            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }
        public async Task<Response>DeleteFileAsync(string url)
        {
            await mediaService.DeleteFileAsync(url);  
            return await Response.SuccessAsync("tmam");
        }
    }
}
