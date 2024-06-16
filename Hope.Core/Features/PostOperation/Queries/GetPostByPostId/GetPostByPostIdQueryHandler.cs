using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPosts;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Queries.GetPostByPostId
{
    internal class GetPostByPostIdQueryHandler : IRequestHandler<GetPostByPostIdQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetPostByPostIdQueryHandler> localizer;
        private readonly IHttpContextAccessor accessor;
        private readonly UserManager<User> userManager;
        public GetPostByPostIdQueryHandler(IUnitofWork work, IStringLocalizer<GetPostByPostIdQueryHandler> localizer, IHttpContextAccessor accessor, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.accessor = accessor;
            this.userManager = userManager;
        }

        public async Task<Response> Handle(GetPostByPostIdQuery query, CancellationToken cancellationToken)
        {
            if (query.IsPeople == null)
            {
                return await Response.FailureAsync("IsPeople Is required");
            }
            var user = await userManager.FindByIdAsync(accessor.HttpContext.User.Claims.FirstOrDefault(i => i.Type == "uid").Value);
            if (user == null) 
            {
                return await Response.FailureAsync(localizer["Faild"]);
            }

            GetAllPostsQueryResponse post;

            if (query.IsPeople)
            {
                post = work.Repository<PostOfLostPeople>().GetItem(i => i.Id == query.PostId).Result.Adapt<GetAllPostsQueryResponse>();
            }
            else
            {
                post = work.Repository<PostOfLostThings>().GetItem(i => i.Id == query.PostId).Result.Adapt<GetAllPostsQueryResponse>();
            }
            if(post == null) 
            {
                return await Response.FailureAsync(localizer["PostNotExist"]);
            }

              return await Response.SuccessAsync(post, localizer["Success"]);
        }
    }
}
