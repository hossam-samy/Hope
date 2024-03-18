using Hope.Core.Common;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Queries.GetReplies
{

    internal class GetRepliesQueryHandler : IRequestHandler<GetRepliesQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetRepliesQueryHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IMapper  mapper;
        public GetRepliesQueryHandler(IUnitofWork work, IStringLocalizer<GetRepliesQueryHandler> localizer, UserManager<User> userManager, IMapper mapper)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        public async Task<Response> Handle(GetRepliesQuery query, CancellationToken cancellationToken)
        {
            var comment = work.Repository<Comment>().Get(i => i.Id == query.Id).Result.FirstOrDefault();

            var comments = comment.Comments;

            if (comment is null)
            {
                return await Response.FailureAsync(localizer["Faild"].Value);
            }

            var response = mapper.Map<List<GetRepliesQueryResponse>>(comments);



            return await Response.SuccessAsync(response, localizer["Success"].Value);

        }
    }
}
