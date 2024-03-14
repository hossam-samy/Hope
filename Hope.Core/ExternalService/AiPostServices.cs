using Hope.Core.Common;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;

namespace Hope.Core.ExternalService
{
    public class AiPostServices : IAiPostServices
    {
        private readonly IUnitofWork work;

        public AiPostServices(IUnitofWork work)
        {
            this.work = work;
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
