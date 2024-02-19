using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Common.Errors
{
    public class PasswordErrors
    {
        public static readonly Error ConfirmedPasswordWrong = Error.Validation("Email.ConfirmedPasswordWrong", "ConfirmedPasswordWrong");

    }
}
