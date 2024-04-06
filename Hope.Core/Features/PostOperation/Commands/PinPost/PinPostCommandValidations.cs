﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.PostOperation.Commands.PinPost
{
    public class PinPostCommandValidations:AbstractValidator<PinPostCommand>
    {
        public PinPostCommandValidations()
        {
            RuleFor(i => i.PostId).NotEmpty().WithMessage("PostId Is Required");
            RuleFor(i => i.IsPeople).NotNull().WithMessage("IsPeople IsRequired");


        }
    }
}