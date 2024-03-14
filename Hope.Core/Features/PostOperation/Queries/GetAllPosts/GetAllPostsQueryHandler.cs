using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPosts
{
    internal class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<GetAllPostsQueryHandler> localizer;
        private readonly IMediaService mediaService;
        private readonly UserManager<User> userManager;
        public GetAllPostsQueryHandler(IUnitofWork work, IMapper mapper, IStringLocalizer<GetAllPostsQueryHandler> localizer, IMediaService mediaService, UserManager<User> userManager)
        {
            this.work = work;
            this.mapper = mapper;
            this.localizer = localizer;
            this.mediaService = mediaService;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(GetAllPostsQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(query.UserId);

            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"]);


            
            var peopleposts = query.Peoplecursor != 0 ? work.Repository<PostOfLostPeople>().
                Get(i => i.Id > query.Peoplecursor && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.Take(16).ToList().Adapt<List<GetAllPostsQueryResponse>>() : new List<GetAllPostsQueryResponse>();

           
            var thingsposts = query.thingcursor != 0 ? work.Repository<PostOfLostThings>().
               Get(i => i.Id > query.thingcursor && !i.HiddenThings.Contains(user), new[] { "HiddenThings" }).Result.Take(16).ToList().Adapt<List<GetAllPostsQueryResponse>>() : new List<GetAllPostsQueryResponse>();




            query.Peoplecursor = peopleposts?.LastOrDefault()?.Id;
            query.thingcursor = thingsposts?.LastOrDefault()?.Id;


            List<GetAllPostsQueryResponse> allposts = [.. peopleposts, .. thingsposts];

            //post1.ToList().ForEach(x => x.UserName = Peopleposts.Select(i => i.Name).FirstOrDefault() ?? x.UserName);


            return await Response.SuccessAsync(new { allposts, query.Peoplecursor, query.thingcursor }, localizer["Success"]);
        }
    }
}
