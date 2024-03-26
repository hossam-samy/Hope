using FluentValidation;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Commands.CreatePostForThings
{
    public class CreatePostForThingsCommandValidation:AbstractValidator<CreatePostForThingsCommand> 
    {
        public CreatePostForThingsCommandValidation(IStringLocalizer<CreatePostForThingsCommandValidation>localizer)
        {

            RuleFor(i => i.MissigDate).MustAsync(async (date, __) => {

                if (!date.IsNullOrEmpty() && !DateTime.TryParse(date,out _))
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
            RuleFor(i => i.Type).NotEmpty().WithMessage(localizer["TypeRequired"].Value);
            RuleFor(i => i.Description).NotEmpty().WithMessage(localizer["DescriptionRequired"].Value);

        }
    }
}
