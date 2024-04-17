using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Commands.PinPost;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor accessor;
        public UpdateCommentCommandHandler(IUnitofWork work, IStringLocalizer<PinPostCommandHandler> localizer, UserManager<User> userManager, IValidator<UpdateCommentCommand> validator, IHttpContextAccessor accessor)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(UpdateCommentCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;

            var user = await userManager.FindByIdAsync(userid!);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"].Value);

            }

            var comment = user.Comments.FirstOrDefault(i => i.Id == command.CommentId);

            if (comment == null) return await Response.FailureAsync(localizer["BlockUpdatingComment"].Value);

            comment.Content = command.Content;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
