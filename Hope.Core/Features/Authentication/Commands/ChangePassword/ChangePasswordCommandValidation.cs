using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommandValidation:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidation(IStringLocalizer<ChangePasswordCommandValidation> localizer)
        {
            RuleFor(i => i.UserEmail).NotNull().WithMessage(localizer["UserEmailRequired"]).NotEmpty().WithMessage(localizer["UserEmailRequired"]);
            RuleFor(i=>i.password).NotNull().WithMessage(localizer["PasswordRequired"]).NotEmpty().WithMessage(localizer["PasswordRequired"]);    
        }
    }
}
