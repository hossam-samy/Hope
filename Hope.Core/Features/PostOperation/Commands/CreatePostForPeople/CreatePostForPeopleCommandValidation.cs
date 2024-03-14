using FluentValidation;
using Hope.Domain.Model;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Commands.CreatePostForPeople
{
    public class CreatePostForPeopleCommandValidation:AbstractValidator<CreatePostForPeopleCommand> 
    {
        public CreatePostForPeopleCommandValidation(IStringLocalizer<CreatePostForPeopleCommandValidation> localizer)
        {
            RuleFor(i => i.PhoneNumber).MustAsync(async (phone, _) =>
            {


                if (phone == null ||
                phone.Length != 11 ||
                !phone.StartsWith("010") ||
                !phone.StartsWith("011") ||
                !phone.StartsWith("012") ||
                !phone.StartsWith("015"))
                    return false;

                return true;
            }).WithMessage(localizer["PhoneNumberRequired"]).NotNull().WithMessage(localizer["PhoneNumberRequired"]).NotEmpty().WithMessage(localizer["PhoneNumberRequired"]);

        }
    }
}
