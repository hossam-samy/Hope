using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;

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
                return await Response.FailureAsync(localizer["UserNotExist"].Value);



            var peopleposts = work.Repository<PostOfLostPeople>().
                Get(i => !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" }).Result.Skip((query.PageNumber - 1) * 16).Take(16).ToList().Adapt<List<GetAllPostsQueryResponse>>();


            var thingsposts = work.Repository<PostOfLostThings>().
               Get(i => !i.HiddenThings.Contains(user), new[] { "HiddenThings" }).Result.Skip((query.PageNumber - 1) * 16).Take(16).ToList().Adapt<List<GetAllPostsQueryResponse>>();



           

            peopleposts.ForEach(i => i.IsPeople = true);


            thingsposts.ForEach(i => i.IsPeople = false);


            //peopleposts.ForEach(x => x.UserName = peopleEntities.Where(i=>i.Id==x.Id).Select(i => i.User.DisplayName??i.User.UserName)?.FirstOrDefault()!);

            //thingsposts.ForEach(x => x.UserName = thingsEntities.Where(i=>i.Id==x.Id).Select(i => i.User.DisplayName??i.User.UserName)?.FirstOrDefault()!);

            List<GetAllPostsQueryResponse> allposts = [.. peopleposts, .. thingsposts];


            return await Response.SuccessAsync(allposts, localizer["Success"].Value);
        }
    }
}
