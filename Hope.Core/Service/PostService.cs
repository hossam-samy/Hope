using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Common;
using Hope.Domain.Common.Consts;
using Hope.Domain.Model;
using Humanizer.Localisation;
using MapsterMapper;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Service
{
    public class PostService:IPostService
    {
        private readonly IUnitofWork work;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<PostService> localizer;
        private readonly IMediaService mediaService;
        public PostService(IUnitofWork work, IMapper mapper, IStringLocalizer<PostService> localizer, IMediaService mediaService)
        {
            this.work = work;
            this.mapper = mapper;
            this.localizer = localizer;
            this.mediaService = mediaService;
        }
        public async Task<Response> AddPostPeople([FromForm]PostPeopleRequest dto)
        {

            if (dto.ImageFile!=null)
                await mediaService.AddFileAsync(dto.ImageFile);   

               
            var post = mapper.Map<PostOfLostPeople>(dto);
            if (dto.ImageFile != null) 
            post.ImageUrl = mediaService.GetUrl()+"\\"+ dto?.ImageFile?.FileName;
           

            await work.Repository<PostOfLostPeople>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }

        public async Task<Response> AddPostThings([FromForm]PostThingsRequest dto)
        {
            if (dto.ImageFile != null)
                await mediaService.AddFileAsync(dto.ImageFile);



            var post = mapper.Map<PostOfLostThings>(dto);
            if (dto.ImageFile != null)
                post.ImageUrl =  mediaService.GetUrl() + "\\" + dto?.ImageFile?.FileName;

            await work.Repository<PostOfLostThings>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);

        }

        public async Task<Response> GetPostsOfAllPeople()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i=>i);

          
            var output = new List<PostPeopleResponse>();
            
            foreach (var item in posts)
            {
                output.Add(new PostPeopleResponse { 
                     Age=item.Age??0, 
                     City=item.City??item.User.City,
                     Condition=item.Condition,
                     CreationDate=item.CreationDate,
                     Description=item.Description,
                     Gendre=item.Gendre,
                     Name=item?.Name,    
                     UserName=item.User.UserName,
                     ImageUrl=item.ImageUrl,
                     UserImage=item.User.UserImage,
                     IsSearcher=item.IsSearcher,
                     MissigDate=item.MissigDate,
                     PhoneNumber=item.PhoneNumber
                });
            }

            

            return await Response.SuccessAsync(output, localizer["Success"].Value);


        }
        public async Task<Response> GetPostOfAccidents()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i=>i.Condition==Condition.accidents);


            var output = new List<PostPeopleResponse>();
            foreach (var item in posts)
            {
                output.Add(new PostPeopleResponse
                {
                    Age = item.Age ?? 0,
                    City = item.City ?? item.User.City,
                    Condition = item.Condition,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    Gendre = item.Gendre,
                    Name = item?.Name,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage,
                    IsSearcher = item.IsSearcher,
                    MissigDate = item.MissigDate,
                    PhoneNumber = item.PhoneNumber
                });
            }

           

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfLosties()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.losties);

           
            var output = new List<PostPeopleResponse>();
            foreach (var item in posts)
            {
                output.Add(new PostPeopleResponse
                {
                    Age = item.Age ?? 0,
                    City = item.City ?? item.User.City,
                    Condition = item.Condition,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    Gendre = item.Gendre,
                    Name = item?.Name,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage,
                    IsSearcher = item.IsSearcher,
                    MissigDate = item.MissigDate,
                    PhoneNumber = item.PhoneNumber
                });
            }

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfShelters()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.shelters);

           
            var output = new List<PostPeopleResponse>();
            foreach (var item in posts)
            {
                output.Add(new PostPeopleResponse
                {
                    Age = item.Age ?? 0,
                    City = item.City ?? item.User.City,
                    Condition = item.Condition,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    Gendre = item.Gendre,
                    Name = item?.Name,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage,
                    IsSearcher = item.IsSearcher,
                    MissigDate = item.MissigDate,
                    PhoneNumber = item.PhoneNumber
                });
            }

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }


        public async Task<Response> GetPostThings()
        {
            var posts = await work.Repository<PostOfLostThings>().Get(i => i);

            var output = new List<PostThingResponse>();
            foreach (var item in posts)
            {
                output.Add(new PostThingResponse
                {
                   
                    City = item.City ?? item.User.City,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage,
                    IsSearcher = item.IsSearcher,
                    MissigDate = item.MissigDate,
                    PhoneNumber = item.PhoneNumber,
                    Type = item.Type   
                });
            }
            
            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }
    }
}
