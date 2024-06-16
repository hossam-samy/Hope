using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Commands.ChangePassword
{
    public class ChangePasswordCommand:IRequest<Response>
    {
        public string UserEmail { get; set; }
        public string password { get; set; }
        
            
    }
}
