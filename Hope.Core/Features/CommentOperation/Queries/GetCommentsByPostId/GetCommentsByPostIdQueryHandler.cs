using Hope.Core.Common;
using Hope.Core.Features.CommentOperation.Queries.GetReplies;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.CommentOperation.Queries.GetCommentsByPostId
{
    internal class GetCommentsByPostIdQueryHandler : IRequestHandler<GetCommentsByPostIdQuery, Response>
    {
        private readonly IUnitofWork work;
        private IStringLocalizer<GetCommentsByPostIdQueryHandler> localizer;

        public GetCommentsByPostIdQueryHandler(IUnitofWork work, IStringLocalizer<GetCommentsByPostIdQueryHandler> localizer)
        {
            this.work = work;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetCommentsByPostIdQuery query, CancellationToken cancellationToken)
        {
            List<GetRepliesQueryResponse> Comments;
            if (query.IsPeople)
            {
                var Post=work.Repository<PostOfLostPeople>().Get(i=>i.Id==query.PostId).Result.FirstOrDefault();    
                 
                if(Post==null) 
                {
                    return await Response.FailureAsync(localizer["PostNotExist"]);
                }

                Comments = Post.Comments.Adapt<List<GetRepliesQueryResponse>>();   
            }
            else
            {

                var Post = work.Repository<PostOfLostThings>().Get(i => i.Id == query.PostId).Result.FirstOrDefault();

                if (Post == null)
                {
                    return await Response.FailureAsync(localizer["PostNotExist"]);
                }

                Comments = Post.Comments.Adapt<List<GetRepliesQueryResponse>>();
            }

            return await Response.SuccessAsync(Comments, localizer["Success"]);
        }
    }
}
