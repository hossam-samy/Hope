﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.Login
{
    public class LoginQueryValidation : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidation()
        {
            RuleFor(i => i.Email).NotNull().WithMessage("The Email Is Required Input").NotEmpty().WithMessage("The Email Is Required Input");

            RuleFor(i => i.Password).NotNull().WithMessage("The Email Is Required Input").NotEmpty().WithMessage("The Email Is Required Input");
        }
    }
}
