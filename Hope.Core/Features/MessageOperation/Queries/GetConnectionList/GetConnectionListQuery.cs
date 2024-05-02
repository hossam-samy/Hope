using Hope.Core.Common;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.MessageOperation.Queries.GetConnectionList
{
    public class GetConnectionListQuery:IRequest<Response>
    {
    }


    public class GetConnectionListQueryHandler : IRequestHandler<GetConnectionListQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor accessor;
        private readonly IStringLocalizer<GetConnectionListQueryHandler> localizer;

        public GetConnectionListQueryHandler(UserManager<User> userManager, IHttpContextAccessor accessor, IStringLocalizer<GetConnectionListQueryHandler> localizer)
        {
            this.userManager = userManager;
            this.accessor = accessor;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetConnectionListQuery query, CancellationToken cancellationToken)
        {
            var userId=accessor.HttpContext!.User.Claims.First(i=>i.Type=="uid").Value;  
            
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }
            var lst1 = user.SentMessages
                .Select(i => new { 
                 i.Recipient.DisplayName,
                 i.Recipient.Id,
                  i.Recipient.UserImage,
                 IsOnline = i.Recipient.UserConnections.Any(x => x.UserId == i.Recipient.Id)
                })
                .Distinct()
                .ToList();   
            var lst2=user.RecievedMessages
                .Select(i => new {
                    i.Sender.DisplayName,
                    i.Sender.Id,
                    i.Sender.UserImage, 
                    IsOnline = i.Sender.UserConnections.Any(x => x.UserId == i.Sender.Id) 
                }).Distinct().ToList();

            return await Response.SuccessAsync(lst1.Union(lst2), localizer["Success"]);
        }
    }
}
