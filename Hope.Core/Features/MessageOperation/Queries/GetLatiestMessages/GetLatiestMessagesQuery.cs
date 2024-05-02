using Hope.Core.Common;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.MessageOperation.Queries.GetLatiestMessages
{
    public class GetLatiestMessagesQuery : IRequest<Response>
    {
    }
    public class GetLatiestMessagesQueryHandler : IRequestHandler<GetLatiestMessagesQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor accessor;
        private readonly IStringLocalizer<GetLatiestMessagesQueryHandler> localizer;


        public GetLatiestMessagesQueryHandler(UserManager<User> userManager, IHttpContextAccessor accessor, IStringLocalizer<GetLatiestMessagesQueryHandler> localizer)
        {
            this.userManager = userManager;
            this.accessor = accessor;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetLatiestMessagesQuery query, CancellationToken cancellationToken)
        {
            var userId = accessor.HttpContext!.User.Claims.FirstOrDefault(i => i.Type == "uid")!.Value;

            var user = await userManager.FindByIdAsync(userId);
            List<GetLatiestMessagesQueryResponse> response = new List<GetLatiestMessagesQueryResponse>();

            var messages1 = user?.SentMessages.OrderByDescending(i => i.Date).GroupBy(i => i.RecipientId).Select(i => i.FirstOrDefault());
            var messages2 = user?.RecievedMessages.OrderByDescending(i => i.Date).GroupBy(i => i.SenderId).Select(i => i.FirstOrDefault());

            var res = messages1?.Union(messages2);

            foreach (var item in res)
            {
                var found = res.FirstOrDefault(i => i.RecipientId == item.SenderId && i.SenderId == item.RecipientId);
                if (found!=null && found.Date > item.Date)
                {
                    response.Add(new GetLatiestMessagesQueryResponse
                    {
                        Content = found.Content,
                        Date = found.Date,
                        DisplayName = found.SenderId == userId ? found.Recipient.DisplayName : found.Sender.DisplayName,
                        UserImage = found.SenderId == userId ? found.Recipient.UserImage : found.Sender.UserImage,
                        Id = found.Id,
                        IsRead = found.IsRead,
                        RecipentId = found.SenderId == userId ? found.RecipientId : found.SenderId,
                    });
  
                }
                else
                    response.Add(new GetLatiestMessagesQueryResponse
                    {
                        Content = item.Content,
                        Date = item.Date,
                        DisplayName = item.SenderId == userId ? item.Recipient.DisplayName : item.Sender.DisplayName,
                        UserImage = item.SenderId == userId ? item.Recipient.UserImage : item.Sender.UserImage,
                        Id = item.Id,
                        IsRead = item.IsRead,
                        RecipentId = item.SenderId == userId ? item.RecipientId : item.SenderId,
                    });
            }


            return await Response.SuccessAsync(response.Distinct(), localizer["Success"].Value);
        }
    }
}
