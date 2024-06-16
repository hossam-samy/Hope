using Hope.Core.Common;
using Hope.Core.Common.Consts;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents
{
    internal class GetAllPostsOfAccidentQueryHandler : IRequestHandler<GetAllPostsOfAccidentsQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor accessor;
        public GetAllPostsOfAccidentQueryHandler(IUnitofWork work, IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer, UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(GetAllPostsOfAccidentsQuery query, CancellationToken cancellationToken)
        {
            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"].Value);

            var posts =  work.Repository<PostOfLostPeople>().
               Get(i =>  i.Condition == Condition.accidents && !i.HiddenPeoples.Contains(user), new[] { "HiddenPeoples" })
               .Result.Skip((query.PageNumber - 1) * 32).Take(32).ToList()?.Adapt<List<GetAllPostsOfPeopleQueryResponse>>() ;

            return await Response.SuccessAsync( posts, localizer["Success"].Value);
        }
    }
}
