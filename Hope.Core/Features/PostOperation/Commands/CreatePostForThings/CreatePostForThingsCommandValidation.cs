﻿using FluentValidation;
using Microsoft.Extensions.Localization;
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

            RuleFor(i => i.PhoneNumber).MustAsync(async (phone, _) =>
            {


                if (phone != null &&
                phone.Length == 11 && (
                phone.StartsWith("010") ||
                phone.StartsWith("011") ||
                phone.StartsWith("012") ||
                phone.StartsWith("015")))
                    return true;

                return false;
            }).WithMessage(localizer["PhoneNumberInvalid"].Value).NotNull().WithMessage(localizer["PhoneNumberRequired"].Value).NotEmpty().WithMessage(localizer["PhoneNumberRequired"].Value);

        }
    }
}
