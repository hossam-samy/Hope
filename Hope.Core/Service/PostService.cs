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
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostService(IUnitofWork work, IMapper mapper, IStringLocalizer<PostService> localizer, IWebHostEnvironment webHostEnvironment)
        {
            this.work = work;
            this.mapper = mapper;
            this.localizer = localizer;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<Response> AddPostPeople([FromForm]PostPeopleRequest dto)
        {

            

            if (dto.ImageFile.Length > 0) {

                string file = Path.Combine(webHostEnvironment.WebRootPath, dto.ImageFile.FileName);
                using (var fileStream = new FileStream(file, FileMode.OpenOrCreate))
                {
                    await dto.ImageFile.CopyToAsync(fileStream);
                }

            }

               
            var post = mapper.Map<PostOfLostPeople>(dto);
            post.ImageUrl = webHostEnvironment.WebRootPath+"\\"+ dto.ImageFile.FileName;
           

            await work.Repository<PostOfLostPeople>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }

        public async Task<Response> AddPostThings([FromForm]PostThingsRequest dto)
        {
            if (dto.ImageFile.Length > 0)
            {
                string file = Path.Combine(webHostEnvironment.WebRootPath, dto.ImageFile.FileName);
                using (var fileStream = new FileStream(file, FileMode.OpenOrCreate))
                {
                    await dto.ImageFile.CopyToAsync(fileStream);
                }

            }

           

            var post = mapper.Map<PostOfLostThings>(dto);
            post.ImageUrl = webHostEnvironment.WebRootPath + "\\" + dto.ImageFile.FileName;

            await work.Repository<PostOfLostThings>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);

        }

        public async Task<Response> GetPostsOfAllPeople()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i=>i);

            //var test=mapper.Map<PostPeopleResponse>(posts.FirstOrDefault());
            var output = new List<PostPeopleResponse>();
            foreach (var item in posts)
            {
                output.Add(new PostPeopleResponse { 
                    Age=item.Age, 
                    City=item.User.City,
                    Condition=item.Condition,
                    CreationDate=item.CreationDate,
                      Description=item.Description,
                       Gendre=item.Gendre,
                         Name=item.Name,    
                          UserName=item.User.UserName,
                          ImageUrl=item.ImageUrl,
                           UserImage=item.User.UserImage
                });
            }

            //var output = mapper.Map<List<PostPeopleResponse>>(posts);

            //output.ForEach(i => i.UserName="asd");

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
                    Age = item.Age,
                    City = item.User.City,
                    Condition = item.Condition,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    Gendre = item.Gendre,
                    Name = item.Name,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage
                });
            }

            //var output = mapper.Map<IEnumerable<PostPeopleResponse>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfLosties()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.losties);

            //var output = mapper.Map<IEnumerable<PostPeopleRequest>>(posts);

            var output = new List<PostPeopleResponse>();
            foreach (var item in posts)
            {
                output.Add(new PostPeopleResponse
                {
                    Age = item.Age,
                    City = item.User.City,
                    Condition = item.Condition,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    Gendre = item.Gendre,
                    Name = item.Name,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage
                });
            }

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfShelters()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.shelters);

            // var output = mapper.Map<IEnumerable<PostPeopleRequest>>(posts);

            var output = new List<PostPeopleResponse>();
            foreach (var item in posts)
            {
                output.Add(new PostPeopleResponse
                {
                    Age = item.Age,
                    City = item.User.City,
                    Condition = item.Condition,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    Gendre = item.Gendre,
                    Name = item.Name,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage
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
                    Type= item.Type,    
                    City = item.User.City,
                    CreationDate = item.CreationDate,
                    Description = item.Description,
                    UserName = item.User.UserName,
                    ImageUrl = item.ImageUrl,
                    UserImage = item.User.UserImage
                });
            }
            // var  output=mapper.Map<IEnumerable<PostThingsRequest>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }
    }
}
