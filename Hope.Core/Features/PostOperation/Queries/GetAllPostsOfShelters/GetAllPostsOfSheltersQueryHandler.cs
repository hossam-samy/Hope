using Hope.Core.Common;
using Hope.Core.Common.Consts;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfShelters
{
    internal class GetAllPostsOfSheltersQueryHandler : IRequestHandler<GetAllPostsOfSheltersQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllPostsOfSheltersQueryHandler> localizer;
        private readonly UserManager<User> userManager;
        public GetAllPostsOfSheltersQueryHandler(IUnitofWork work, IStringLocalizer<GetAllPostsOfSheltersQueryHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(GetAllPostsOfSheltersQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(query.UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);

            var posts = query.cursor != 0 ? work.Repository<PostOfLostPeople>().
               Get(i => i.Id > query.cursor && i.Condition == Condition.shelters && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.Take(32).ToList().Adapt<List<PostPeopleResponse>>() : new List<PostPeopleResponse>();

            query.cursor = posts?.LastOrDefault()?.Id;

            return await Response.SuccessAsync(new { posts, query.cursor }, localizer["Success"].Value);
        }
    }
}
