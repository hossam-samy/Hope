using FluentValidation;
using Hope.Core.Dtos;
using Hope.Core.Interfaces;
using Hope.Domain.Model;

namespace Hope.Core.Validations
{
    public class UserDtoValidation:AbstractValidator<UserDto>
    {
        public UserDtoValidation(IUnitofWork work)
        {
            RuleFor(i => i.UserName).MustAsync(async (username, _) => {

                if (work.Repository<Domain.Model.User>().Get(i => i.UserName == username).Result.FirstOrDefault() != null) return false;


                return true;   
            
            
            }).WithMessage("UserName Should Be Unique").NotEmpty().WithMessage("The UserName Is Required Input").NotNull().WithMessage("The UserName Is Required Input");
            
            RuleFor(i=>i.Name).NotNull().WithMessage("The Name Is Required Input").NotEmpty().WithMessage("The Name Is Required Input");
           
            RuleFor(i=>i.City).NotNull().WithMessage("The City Is Required Input").NotEmpty().WithMessage("The City Is Required Input");


            RuleFor(i => i.PhoneNumber).MustAsync(async (phone,_) =>
            {


                if (phone == null ||
                phone.Length != 11 ||
                !phone.StartsWith("010") ||
                !phone.StartsWith("011") ||
                !phone.StartsWith("012") ||
                !phone.StartsWith("015"))
                    return false;

                return true;    
            }).WithMessage("PhoneNumber is in Invalid Format").MustAsync(async (phone, _) =>
            {
                if (work.Repository<User>().Get(i => i.PhoneNumber == phone).Result.FirstOrDefault() != null) return false;

                return true;

            }).WithMessage("PhoneNumber Should Be Unique").NotNull().WithMessage("The PhoneNumber Is Required Input").NotEmpty().WithMessage("PhoneNumber Should Be Unique");


            RuleFor(i => i.Email).MustAsync(async (email, _) =>
            {

                if (!email.EndsWith("@gmail.com")) return false;

                return true;

            }).WithMessage("Email is in Invalid Format").MustAsync(async (email,_) => {

                if (work.Repository<User>().Get(i => i.Email == email).Result.FirstOrDefault() != null) return false;

                return true;


            }).WithMessage("Email should be Unique").NotNull().WithMessage("The Email Is Required Input").NotEmpty().WithMessage("The Email Is Required Input");

            RuleFor(i =>  i.Password ).NotEmpty().WithMessage("The Password Is Required Input").NotNull().WithMessage("The Password Is Required Input").
                MustAsync(async (password, _) => {

                    var c = 0;

                    var starts = new char[] {'A','!','a','0'};
                    var ends = new char[] { 'Z', '/', 'z', '9' };

                    for (int i = 0; i < 4; i++)
                    {   
                        bool f = false;
                        if (i == 1)
                        {
                            for (char j = starts[i]; j <= ends[i]; j++)
                                if (password.Contains(j))
                                    f = true;
                            for (char j = ':'; j <= '@'; j++)
                                if (password.Contains(j))
                                    f = true;
                            for (char j = '['; j <= '\''; j++)
                                if (password.Contains(j))
                                    f = true;
                            for (char j = '{'; j <= '}'; j++)
                                if (password.Contains(j))
                                    f = true;
                        }
                        else
                        for (char j = starts[i]; j <= ends[i]; j++)
                            if (password.Contains(j))
                                f = true;

                        c = f ? c++ : c;

                    }


                    

                    if (password.Length < 8 ||c!=4)
                        return false;

                    return true;  
                }).WithMessage("Password should be at least 8 chars contains Symbols ,Capital Letters,Small Letters and Numbers ");



        }
    }
}
