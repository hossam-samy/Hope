using Hope.Core.Common;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Hope.Core.ExternalService
{
    public class AiPostServices : IAiPostServices
    {
        private readonly IUnitofWork work;
        private readonly IMapper mapper;

        public AiPostServices(IUnitofWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }

        public async Task<Response> GetPostOfPeopleByAge(int age)
        {
           var posts = work.Repository<PostOfLostPeople>().IgnoreFilter().Result.Where(i => i.Age == age).ToList().Adapt<List<AiPostPeopleResposnse>>();
            

            return await Response.SuccessAsync(posts, "Success");
        }

        public async Task<Response> GetPostOfPeopleByTown(string town)
        {
            var posts = work.Repository<PostOfLostPeople>().IgnoreFilter().Result.Where(i => i.Town == town).ToList().Adapt<List<AiPostPeopleResposnse>>();
           

            return await Response.SuccessAsync(posts,"Success");
        }

        public async Task<Response> GetPostOfThingsByTown(string town)
        {
            var posts = work.Repository<PostOfLostThings>().IgnoreFilter().Result.Where(i => i.Town == town).ToList().Adapt<List<AiPostPeopleResposnse>>();
          

            return await Response.SuccessAsync(posts, "Success");
        }
    }
}
