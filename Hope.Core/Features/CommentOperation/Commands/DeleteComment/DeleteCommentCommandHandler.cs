using FluentValidation;
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
    internal class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<DeleteCommentCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IValidator<DeleteCommentCommand> validator;
        public DeleteCommentCommandHandler(IUnitofWork work, IStringLocalizer<DeleteCommentCommandHandler> localizer, UserManager<User> userManager, IValidator<DeleteCommentCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
        }
        public async  Task<Response> Handle(DeleteCommentCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var user = await userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"].Value);

            }

            var comment = user.Comments.FirstOrDefault(i => i.Id == command.CommentId);

            if (comment == null) return await Response.FailureAsync(localizer["BlockDeletingComment"].Value);


            await work.Repository<Comment>().Delete(comment);


            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
