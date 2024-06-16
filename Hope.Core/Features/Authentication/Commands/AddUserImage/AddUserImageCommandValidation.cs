using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.AddUserImage
{
    public class AddUserImageCommandValidation : AbstractValidator<AddUserImageCommand>
    {
        public AddUserImageCommandValidation(IStringLocalizer<AddUserImageCommandValidation> localizer)
        {
             //RuleFor(i => i.UserId).NotNull().WithMessage("The UserId Is Required Input").NotEmpty().WithMessage("The UserId Is Required Input");
            RuleFor(i => i.Image).NotEmpty().WithMessage(localizer["ImageRequired"].Value);
      
        }
    }
}
