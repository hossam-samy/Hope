using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Queries.GetReplies
{

    internal class GetRepliesQueryHandler : IRequestHandler<GetRepliesQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<GetRepliesQueryHandler> localizer;
        public GetRepliesQueryHandler(IUnitofWork work, IStringLocalizer<GetRepliesQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }
        public async Task<Response> Handle(GetRepliesQuery query, CancellationToken cancellationToken)
        {
            var comment = await work.Repository<Comment>().GetItem(i => i.Id == query.Id);

            if (comment is null)
            {
                return await Response.FailureAsync(localizer["Faild"].Value);
            }

          // var comments = comment.Comments.Adapt<GetRepliesQueryResponse>();
           var response = comment?.Comments?.Adapt<List<GetRepliesQueryResponse>>();


           

            return await Response.SuccessAsync(response, localizer["Success"].Value);

        }
    }
}
