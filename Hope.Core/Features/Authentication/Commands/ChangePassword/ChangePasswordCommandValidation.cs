using FluentValidation;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommandValidation:AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidation(IStringLocalizer<ChangePasswordCommandValidation> localizer,UserManager<User> userManager)
        {
            RuleFor(i => i.UserEmail).MustAsync( (email, _) =>
            {

                if (!email.EndsWith("@gmail.com")) return Task.FromResult( false);

                return Task.FromResult(true);

            }).WithMessage(localizer["EmailInvalid"].Value).MustAsync(async (email, _) => {

                if (await userManager.FindByEmailAsync(email)== null) return false;

                return true;


            }).WithMessage(localizer["WrongEmail"].Value).NotEmpty().WithMessage(localizer["EmailRequired"].Value);
            RuleFor(i => i.password).NotEmpty().WithMessage(localizer["PasswordRequired"].Value).
                     MustAsync(async (password, _) => {

                         var c = 0;

                         var starts = new char[] { 'a', '!', 'a', '0' };
                         var ends = new char[] { 'z', '/', 'z', '9' };

                         for (int i = 0; i < 4; i++)
                         {
                             bool f = false;
                             if (i == 1)
                             {
                                 for (char j = starts[i]; j <= ends[i]; j++)
                                     if (password.Contains(j))
                                     {
                                         f = true;
                                         break;
                                     }



                                 for (char j = ':'; j <= '@'; j++)
                                     if (password.Contains(j))
                                     {
                                         f = true;
                                         break;
                                     }



                                 for (char j = '['; j <= '\''; j++)
                                     if (password.Contains(j))
                                     {
                                         f = true;
                                         break;
                                     }



                                 for (char j = '{'; j <= '}'; j++)
                                     if (password.Contains(j))
                                     {
                                         f = true;
                                         break;
                                     }
                             }
                             else
                                 for (char j = starts[i]; j <= ends[i]; j++)
                                     if (password.Contains(j))
                                         f = true;

                             if (f) c++;
                         }

                         if (password.Length < 8 || c != 4)
                             return false;

                         return true;
                     }).WithMessage(localizer["PasswordInvalid"].Value);

        }
    }
}
