using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.MessageOperation.Commands.UpdateMessage
{
    public class UpdateMessageCommand:IRequest<Response>
    {
        public int MessageId { get; set; }
        public string? Content { get; set; }
        public bool? IsRead { get; set; } 

    }
    public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UpdateMessageCommandHandler> localizer;
        private readonly IHttpContextAccessor accessor;
        private readonly UserManager<User> userManager;

        public UpdateMessageCommandHandler(IStringLocalizer<UpdateMessageCommandHandler> localizer, IHttpContextAccessor accessor, UserManager<User> userManager, IUnitofWork work )
        {
            this.localizer = localizer;
            this.accessor = accessor;
            this.userManager = userManager;
            this.work = work;
        }

        public async Task<Response> Handle(UpdateMessageCommand command, CancellationToken cancellationToken)
        {
            var user=await userManager.FindByIdAsync(accessor.HttpContext!.User.Claims.First(i=>i.Type=="uid").Value);  
            
            if (user==null) {
             
                return await Response.FailureAsync(localizer["UserNotExist"].Value);    
            
            }

            var message=user.SentMessages.FirstOrDefault(i=>i.Id==command.MessageId);   

            if(message==null) {
            
                return await Response.FailureAsync("You are not allowed to update this message");    
             
            }


            message.Content=command.Content??message.Content;
            message.IsRead=command.IsRead??message.IsRead;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);    
        }
    }
}
