using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToComment
{
    public class AddCommentToCommentCommandValidation:AbstractValidator<AddCommentToCommentCommand>   
    {
        public AddCommentToCommentCommandValidation(IStringLocalizer<AddCommentToCommentCommandValidation> localizer)
        {
            RuleFor(i => i.Content).NotEmpty().WithMessage(localizer["ContentRequired"].Value);

            RuleFor(i => i.CommentId).NotEmpty().WithMessage("CommentId is Required");
        }
    }
}
