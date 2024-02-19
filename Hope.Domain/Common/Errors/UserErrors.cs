using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Common.Errors
{
    public class UserErrors
    {
        public static Error NotFoundById(string Id) =>
            Error.NotFound("User.NotFound", $"{Id} is not found");
        public static Error NotFoundByEmail(string Email) =>
            Error.NotFound("User.NotFound", $"{Email} is not found");
        public static Error InvalidCredintial() =>
           Error.Validation("User.InvalidCredintial", $"Username or Password is wrong");
    }
}
