using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Commands.PinPost;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Commands.UpdateComment
{
    internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<PinPostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IValidator<UpdateCommentCommand> validator;
        public UpdateCommentCommandHandler(IUnitofWork work, IStringLocalizer<PinPostCommandHandler> localizer, UserManager<User> userManager, IValidator<UpdateCommentCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
        }
        public async Task<Response> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"]);
            }

            var user = await userManager.FindByIdAsync(command.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var comment = user.Comments.FirstOrDefault(i => i.Id == command.CommentId);

            if (comment == null) return await Response.FailureAsync(localizer["BlockUpdatingComment"]);

            comment.Content = command.Content;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);
        }
    }
}
