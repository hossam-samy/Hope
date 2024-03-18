using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToComment
{
    public class AddCommentToCommentCommandValidation:AbstractValidator<AddCommentToCommentCommand>   
    {
        public AddCommentToCommentCommandValidation(IStringLocalizer<AddCommentToCommentCommandValidation> localizer)
        {
            RuleFor(i => i.Content).NotNull().WithMessage(localizer["ContentRequired"].Value).NotEmpty().WithMessage(localizer["ContentRequired"].Value);

            RuleFor(i => i.CommentId).NotNull().WithMessage("CommentId is Required").NotEmpty().WithMessage("CommentId is Required");
        }
    }
}
