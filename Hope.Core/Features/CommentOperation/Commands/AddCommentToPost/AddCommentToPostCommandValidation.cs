using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToPost
{
    public class AddCommentToPostCommandValidation:AbstractValidator<AddCommentToPostCommand>
    {
        public AddCommentToPostCommandValidation(IStringLocalizer<AddCommentToPostCommand> localizer)
        {
            RuleFor(i => i.Content).NotNull().WithMessage(localizer["ContentRequired"]).NotEmpty().WithMessage(localizer["ContentRequired"]);

            RuleFor(i => i.PostId).NotNull().WithMessage("PostId is Required").NotEmpty().WithMessage("PostId is Required");
           
            RuleFor(i => i.IsPeople).NotNull().WithMessage("IsPeople is Required").NotEmpty().WithMessage("IsPeople is Required");

        }
    }
}
