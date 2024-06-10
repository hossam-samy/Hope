using FluentValidation;
using Hope.Core.Common.Consts;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace Hope.Core.Features.PostOperation.Commands.CreatePostForPeople
{
    public class CreatePostForPeopleCommandValidation:AbstractValidator<CreatePostForPeopleCommand> 
    {
        public CreatePostForPeopleCommandValidation(IUnitofWork work,IStringLocalizer<CreatePostForPeopleCommandValidation> localizer)
        {
            RuleFor(i => i.MissigDate).MustAsync(async (date, _) => {

                if (!date.IsNullOrEmpty() && !DateTime.TryParse(date, out DateTime result))
                    return false;

                return true;

            }).WithMessage("MissingDate is invalid format");

            RuleFor(i => i.PhoneNumber).MustAsync(async (phone, _) =>
            {

                if (phone.IsNullOrEmpty() ||
                phone.Length == 11 && (
                phone.StartsWith("010") ||
                phone.StartsWith("011") ||
                phone.StartsWith("012") ||
                phone.StartsWith("015")))
                    return true;

                return false;
            }).WithMessage(localizer["PhoneNumberInvalid"].Value);

            RuleFor(i => i.City).NotEmpty().WithMessage(localizer["CityRequired"].Value);
            RuleFor(i => i.Town).NotEmpty().WithMessage(localizer["TownRequired"].Value);
            RuleFor(i => i.IsSearcher).NotNull().WithMessage(localizer["IsSearcherRequired"].Value);
            RuleFor(i => i.Condition).NotEmpty().WithMessage(localizer["ConditionRequired"].Value);
            RuleFor(i => i.Gendre).NotEmpty().WithMessage(localizer["GendreRequired"].Value);
            RuleFor(i => i.Description).NotEmpty().WithMessage(localizer["DescriptionRequired"].Value);

        }
    }
}
