using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Common;
using Hope.Domain.Common.Consts;
using Hope.Domain.Model;
using MapsterMapper;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Service
{
    public class PostService:IPostService
    {
        private readonly IUnitofWork work;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<PostService> localizer;
        public PostService(IUnitofWork work, IMapper mapper, IStringLocalizer<PostService> localizer)
        {
            this.work = work;
            this.mapper = mapper;
            this.localizer = localizer;
        }

        public async Task<Response> AddPostPeople(PostPeopleDto dto)
        {
            var post = mapper.Map<PostOfLostPeople>(dto);

            await work.Repository<PostOfLostPeople>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }

        public async Task<Response> AddPostThings(PostThingsDto dto)
        {
            var post = mapper.Map<PostOfLostThings>(dto);

            await work.Repository<PostOfLostThings>().AddAsync(post);

            return await Response.SuccessAsync(localizer["Success"].Value);

        }

        public async Task<Response> GetPostsOfAllPeople()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i=>i);

            var output = mapper.Map<IEnumerable<PostPeopleDto>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);


        }
        public async Task<Response> GetPostOfAccidents()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i=>i.Condition==Condition.accidents);

            var output = mapper.Map<IEnumerable<PostPeopleDto>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfLosties()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.losties);

            var output = mapper.Map<IEnumerable<PostPeopleDto>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }

        public async Task<Response> GetPostOfShelters()
        {
            var posts = await work.Repository<PostOfLostPeople>().Get(i => i.Condition == Condition.shelters);
            
            var output = mapper.Map<IEnumerable<PostPeopleDto>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }


        public async Task<Response> GetPostThings()
        {
            var posts = await work.Repository<PostOfLostThings>().Get(i => i);
            
            var  output=mapper.Map<IEnumerable<PostThingsDto>>(posts);

            return await Response.SuccessAsync(output, localizer["Success"].Value);
        }
    }
}
