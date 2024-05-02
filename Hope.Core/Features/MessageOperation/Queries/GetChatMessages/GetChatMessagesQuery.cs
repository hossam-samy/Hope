using Hope.Core.Common;
using Hope.Core.Features.MessageOperation.Commands.UpdateMessage;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.MessageOperation.Queries.GetChatMessages
{
    public class GetChatMessagesQuery:IRequest<Response>
    {
        public string RecipientId { get; set; }
    }
    public class GetChatMessagesQueryHandler : IRequestHandler<GetChatMessagesQuery, Response>
    {
        private readonly IUnitofWork work;
        private readonly IHttpContextAccessor accessor;
        private readonly IStringLocalizer<GetChatMessagesQueryHandler> localizer;


        public GetChatMessagesQueryHandler(IUnitofWork work, IHttpContextAccessor accessor, IStringLocalizer<GetChatMessagesQueryHandler> localizer)
        {
            this.work = work;
            this.accessor = accessor;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetChatMessagesQuery query, CancellationToken cancellationToken)
        {
            var userId = accessor.HttpContext!.User.Claims.First(i => i.Type == "uid").Value;
            var messages =  work.Repository<Message>().Get(i => i.SenderId == userId && i.RecipientId == query.RecipientId)
                .Result.OrderByDescending(i=>i.Date).Adapt<IEnumerable<GetChatMessagesQueryResponse>>();

            return await Response.SuccessAsync(messages, localizer["Success"].Value);
        }
    }
}
