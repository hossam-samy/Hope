using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.MessageOperation.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest<Response>
    {
        public int MessageId { get; set; }
    }
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IHttpContextAccessor accessor;
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<DeleteMessageCommandHandler> localizer;


        public DeleteMessageCommandHandler(UserManager<User> userManager, IHttpContextAccessor accessor, IStringLocalizer<DeleteMessageCommandHandler> localizer, IUnitofWork work)
        {
            this.userManager = userManager;
            this.accessor = accessor;
            this.localizer = localizer;
            this.work = work;
        }

        public async Task<Response> Handle(DeleteMessageCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(accessor.HttpContext!.User.Claims.First(i => i.Type == "uid").Value);

            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"].Value);

            }
            var message = user.SentMessages.FirstOrDefault(i => i.Id == command.MessageId);

            if (message == null)
            {

                return await Response.FailureAsync("You are not allowed to delete this message");

            }


            message.IsDeleted = true;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
