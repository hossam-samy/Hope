using FluentValidation;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.Register
{
    public class RegisterCommandValidation:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation(IUnitofWork work,IStringLocalizer<RegisterCommand> localizer,UserManager<User>  userManager)
        { 
            
            


            RuleFor(i => i.UserName)
                .NotEmpty().WithMessage(localizer["UserNameRequired"].Value)
                .MustAsync(async (username, _) => {

               if (await userManager.FindByNameAsync(username)== null) return true;


                return false;


            }).WithMessage(localizer["UniqueUserName"].Value);

            RuleFor(i => i.DisplayName).NotEmpty().WithMessage(localizer["DisplayNameRequired"].Value);

            RuleFor(i => i.City).NotEmpty().WithMessage(localizer["CityRequired"].Value);


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
            }).WithMessage(localizer["PhoneNumberInvalid"].Value).MustAsync(async (phone, _) =>
            {
                if (work.Repository<User>().Get(i => i.PhoneNumber == phone).Result.FirstOrDefault() != null) return false;

                return true;

            }).WithMessage(localizer["UniquePhoneNumber"].Value).NotEmpty().WithMessage(localizer["PhoneNumberRequired"].Value);


            RuleFor(i => i.Email).MustAsync(async (email, _) =>
            {

                if (!email.EndsWith("@gmail.com")) return false;

                return true;

            }).WithMessage(localizer["EmailInvalid"].Value).MustAsync(async (email, _) => {

                if (await userManager.FindByEmailAsync(email)== null) return true;

                return false;


            }).WithMessage(localizer["UniqueEmail"].Value).NotEmpty().WithMessage(localizer["EmailRequired"].Value);

            RuleFor(i => i.Password).NotEmpty().WithMessage(localizer["PasswordRequired"].Value).
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
                                    if (password.Contains(j)) { 
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
                    }).WithMessage(localizer["PasswordInvalid"].Value).Equal(i => i.ConfirmPassword).WithMessage(localizer["PasswordDidntMatch"]);


        }

    }
}

