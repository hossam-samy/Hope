﻿using Hope.Core.Dtos;
using Hope.Domain.Common;

namespace Hope.Core.Interfaces
{
    public interface IPostService
    {
        public Task<Response> AddPostPeople(PostPeopleRequest dto);
        public Task<Response> AddPostThings(PostThingsRequest dto);
        public Task<Response> GetPostThings();
        public Task<Response> GetPostsOfAllPeople();
        public Task<Response> GetPostOfShelters();
        public Task<Response> GetPostOfAccidents();
        public Task<Response> GetPostOfLosties();
    }
}
