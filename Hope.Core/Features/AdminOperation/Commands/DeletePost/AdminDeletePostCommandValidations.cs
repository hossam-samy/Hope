using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.AdminOperation.Commands.DeletePost
{
    public class AdminDeletePostCommandValidations:AbstractValidator<AdminDeletePostCommand>
    {
        public AdminDeletePostCommandValidations(IStringLocalizer<AdminDeletePostCommandValidations> localizer)
        {
            RuleFor(i => i.PostId).NotEmpty().WithMessage("PostId Is Required");
            RuleFor(i => i.IsPeople).NotNull().WithMessage("IsPeople IsRequired");

        }
    }
}
