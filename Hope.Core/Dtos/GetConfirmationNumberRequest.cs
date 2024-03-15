using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Dtos
{
    public record GetConfirmationNumberRequest(string UserEmail, string num);
}
