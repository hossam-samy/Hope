using FluentValidation;
using Hope.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Validations
{
    public class LoginRequestValidation:AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(i=>i.Email).NotNull().WithMessage("The Email Is Required Input").NotEmpty().WithMessage("The Email Is Required Input");
            
            RuleFor(i=>i.Password).NotNull().WithMessage("The Email Is Required Input").NotEmpty().WithMessage("The Email Is Required Input");
        }
    }
}
