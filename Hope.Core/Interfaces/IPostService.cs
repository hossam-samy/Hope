using Hope.Core.Dtos;
using Hope.Domain.Common;

namespace Hope.Core.Interfaces
{
    public interface IPostService
    {
        public Task<Response> AddPostPeople(PostPeopleDto dto);
        public Task<Response> AddPostThings(PostThingsDto dto);
        public Task<Response> GetPostThings();
        public Task<Response> GetPostsOfAllPeople();
        public Task<Response> GetPostOfShelters();
        public Task<Response> GetPostOfAccidents();
        public Task<Response> GetPostOfLosties();
    }
}
