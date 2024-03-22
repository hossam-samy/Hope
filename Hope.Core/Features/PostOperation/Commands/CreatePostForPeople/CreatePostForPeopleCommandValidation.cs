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


                if (phone != null &&
                phone.Length == 11 &&(
                phone.StartsWith("010") ||
                phone.StartsWith("011") ||
                phone.StartsWith("012") ||
                phone.StartsWith("015")))
                    return true;

               return false;
            }).WithMessage(localizer["PhoneNumberInvalid"].Value).NotEmpty().WithMessage(localizer["PhoneNumberRequired"].Value);

            RuleFor(i => i.City).NotEmpty().WithMessage(localizer["CityRequired"].Value);
            RuleFor(i => i.Town).NotEmpty().WithMessage(localizer["TownRequired"].Value);
            RuleFor(i => i.IsSearcher).NotNull().WithMessage(localizer["IsSearcher Required"].Value);
            RuleFor(i => i.Condition).NotEmpty().WithMessage(localizer["ConditionRequired"].Value);
            RuleFor(i => i.Gendre).NotEmpty().WithMessage(localizer["GendreRequired"].Value);
            RuleFor(i => i.Description).NotEmpty().WithMessage(localizer["DescriptionRequired"].Value);

        }
    }
}
