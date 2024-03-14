using FluentValidation;
using Hope.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Validations
{
    public class AddImageRequestValidation:AbstractValidator<AddImageRequest>
    {
        public AddImageRequestValidation()
        {
           RuleFor(i=>i.UserId).NotNull().WithMessage("The UserId Is Required Input").NotEmpty().WithMessage("The UserId Is Required Input"); 
           RuleFor(i=>i.Image).NotNull().WithMessage("The Image Is Required Input").NotEmpty().WithMessage("The Image Is Required Input"); 
        }
    }
}
