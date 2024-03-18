using FluentValidation;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Commands.Register
{
    public class RegisterCommandValidation:AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidation(IUnitofWork work,IStringLocalizer<RegisterCommand> localizer)
        { 
            
            RuleFor(i => i.UserName).NotNull().WithMessage(localizer["UserNameRequired"].Value)
                .NotEmpty().WithMessage(localizer["UserNameRequired"].Value)
                .MustAsync(async (username, _) => {

               if (work.Repository<User>().Get(i => i.UserName == username).Result.FirstOrDefault()!= null) return false;


                return true;


            }).WithMessage(localizer["UniqueUserName"].Value);

            RuleFor(i => i.DisplayName).NotNull().WithMessage(localizer["DisplayNameRequired"].Value).NotEmpty().WithMessage(localizer["DisplayNameRequired"].Value);

            RuleFor(i => i.City).NotNull().WithMessage(localizer["CityRequired"].Value).NotEmpty().WithMessage(localizer["CityRequired"].Value);


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

            }).WithMessage(localizer["UniquePhoneNumber"].Value).NotNull().WithMessage(localizer["PhoneNumberRequired"].Value).NotEmpty().WithMessage(localizer["PhoneNumberRequired"].Value);


            RuleFor(i => i.Email).MustAsync(async (email, _) =>
            {

                if (!email.EndsWith("@gmail.com")) return false;

                return true;

            }).WithMessage(localizer["EmailInvalid"].Value).MustAsync(async (email, _) => {

                if (work.Repository<User>().Get(i => i.Email == email).Result.FirstOrDefault() != null) return false;

                return true;


            }).WithMessage(localizer["UniqueEmail"].Value).NotNull().WithMessage(localizer["EmailRequired"].Value).NotEmpty().WithMessage(localizer["EmailRequired"].Value);

            RuleFor(i => i.Password).NotEmpty().WithMessage(localizer["PasswordRequired"].Value).NotNull().WithMessage(localizer["PasswordRequired"].Value).
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

                            if (f) c++;                        }

                        if (password.Length < 8 || c != 4)
                            return false;

                       return true;
                   }).WithMessage(localizer["PasswordInvalid"].Value);



        }

    }
}

