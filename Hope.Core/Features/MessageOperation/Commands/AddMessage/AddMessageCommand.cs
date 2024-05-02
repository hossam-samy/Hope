using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Hope.Core.Features.MessageOperation.Commands.AddMessage
{
    public class AddMessageCommand:IRequest<Response>
    {
      
        public string Content { get; set; }

        public string RecipientId { get; set; }
        
    }
    public class AddMessageCommandHandler : IRequestHandler<AddMessageCommand, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor accessor;
        private readonly IUnitofWork work;

        public AddMessageCommandHandler(UserManager<User> userManager, IHttpContextAccessor accessor, IUnitofWork work)
        {
            this.userManager = userManager;
            this.accessor = accessor;
            this.work = work;
        }

        public async Task<Response> Handle(AddMessageCommand command, CancellationToken cancellationToken)
        {
            var userid = accessor.HttpContext!.User.FindFirst("uid")!.Value;
            var user=await userManager.FindByIdAsync(userid);
            var rec= await userManager.FindByIdAsync(command.RecipientId);

            var asd = command.Adapt<Message>();
            asd.SenderId = userid;
            
            user.SentMessages.Add(asd);
            
            rec.RecievedMessages.Add(asd);

            await work.SaveAsync();

            return await Response.SuccessAsync("Success");
        }
    }
}
