using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.Login
{
    public class LoginQueryValidation : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidation(IStringLocalizer<LoginQuery> localizer)
        {
            RuleFor(i => i.Email).NotNull().WithMessage(localizer["EmailRequired"]).NotEmpty().WithMessage(localizer["EmailRequired"]);

            RuleFor(i => i.Password).NotNull().WithMessage(localizer["PasswordRequired"]).NotEmpty().WithMessage(localizer["PasswordRequired"]);
        }
    }
}
