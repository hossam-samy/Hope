using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPosts
{
    internal class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllPostsQueryHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor accessor;
        private readonly IRecommendationService recommendationService;
        public GetAllPostsQueryHandler(IUnitofWork work, IStringLocalizer<GetAllPostsQueryHandler> localizer, UserManager<User> userManager, IHttpContextAccessor accessor, IRecommendationService recommendationService)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.accessor = accessor;
            this.recommendationService = recommendationService;
        }
        public async Task<Response> Handle(GetAllPostsQuery query, CancellationToken cancellationToken)
        {
            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;
            var user = await userManager.FindByIdAsync(userid!);
            var loca = await work.Repository<Location>().Get(i => i);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"].Value);



            var peopleposts = work.Repository<PostOfLostPeople>().
                Get(i => !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.ToList();


            foreach (var item in peopleposts)
            {
                var l = loca.FirstOrDefault(i => i.City == item.City);
                if(l == null) continue; 
                item.Cluster = await recommendationService.predict(l.Longitude,l.Latitude);
            }

            await work.SaveAsync();

            //var thingsposts = work.Repository<PostOfLostThings>().
            //   Get(i => !i.HiddenThings.Contains(user), new[] { "HiddenThings" }).Result.Skip((query.PageNumber - 1) * 16).Take(16).ToList()?.Adapt<List<GetAllPostsQueryResponse>>();





            //peopleposts.ForEach(i => i.IsPeople = true);


            //thingsposts.ForEach(i => i.IsPeople = false);


            //peopleposts.ForEach(x => x.UserName = peopleEntities.Where(i=>i.Id==x.Id).Select(i => i.User.DisplayName??i.User.UserName)?.FirstOrDefault()!);

            //thingsposts.ForEach(x => x.UserName = thingsEntities.Where(i=>i.Id==x.Id).Select(i => i.User.DisplayName??i.User.UserName)?.FirstOrDefault()!);

            //  List<GetAllPostsQueryResponse> allposts = [.. peopleposts, .. thingsposts];


            return await Response.SuccessAsync( localizer["Success"].Value);
        }
    }
}
