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
            RuleFor(i => i.Content).NotNull().WithMessage(localizer["ContentRequired"].Value).NotEmpty().WithMessage(localizer["ContentRequired"].Value);

           

        }
    }
}
