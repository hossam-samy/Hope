using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Commands.DeletePost
{
    public class DeletePostCommandValidations:AbstractValidator<DeletePostCommand>
    {
        public DeletePostCommandValidations(IStringLocalizer<DeletePostCommandValidations> localizer)
        {
            RuleFor(i => i.PostId).NotEmpty().WithMessage("PostId Is Required");
            RuleFor(i => i.IsPeople).NotNull().WithMessage("IsPeople IsRequired");

        }
    }
}
