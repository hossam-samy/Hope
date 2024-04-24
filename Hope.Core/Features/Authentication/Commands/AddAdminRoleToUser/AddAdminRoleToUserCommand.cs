using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Commands.AddAdminRoleToUser
{
    public class AddAdminRoleToUserCommand:IRequest<Response>
    {
        public string UserId { get; set; }
    }
}
