using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.CommentOperation.Commands.UpdateComment
{
    public class UpdateCommentCommandValidation:AbstractValidator<UpdateCommentCommand> 
    {
        public UpdateCommentCommandValidation(IStringLocalizer<UpdateCommentCommand> localizer)
        {
            RuleFor(i => i.CommentId).NotNull().WithMessage("CommentId is Required").NotEmpty().WithMessage("CommentId is Required");

            RuleFor(i => i.Content).NotNull().WithMessage(localizer["ContentRequired"]).NotEmpty().WithMessage(localizer["ContentRequired"]);
        }
    }
}
