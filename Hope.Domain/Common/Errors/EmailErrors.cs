using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Domain.Common.Errors
{
    public class EmailErrors
    {
        public static readonly Error DuplicateUserEmail = Error.Validation("Email.DuplicateEmail", "Email already exist");

    }
}
