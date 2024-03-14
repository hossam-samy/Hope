using Hope.Core.Common;
using Hope.Core.Features.CommentOperation.Commands.UpdateComment;
using Hope.Core.Features.PostOperation.Commands.PinPost;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Org.BouncyCastle.Asn1.Ocsp;

namespace Hope.Core.Features.CommentOperation.Commands.DeleteComment
{
    internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UpdateCommentCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        public UpdateCommentCommandHandler(IUnitofWork work, IStringLocalizer<UpdateCommentCommandHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async  Task<Response> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var comment = user.Comments.FirstOrDefault(i => i.Id == command.CommentId);

            if (comment == null) return await Response.FailureAsync("You are not allowed to delete this Comment");


            await work.Repository<Comment>().Delete(comment);


            return await Response.SuccessAsync(localizer["Success"]);
        }
    }
}
