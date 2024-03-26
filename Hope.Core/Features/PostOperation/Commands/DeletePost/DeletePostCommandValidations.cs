using FluentValidation;
using Microsoft.Extensions.Localization;

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
