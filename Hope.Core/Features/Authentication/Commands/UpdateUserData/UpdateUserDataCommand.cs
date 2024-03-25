using Hope.Core.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Commands.UpdateUserData
{
    public class UpdateUserDataCommand:IRequest<Response>
    {

        public string? UserId { get; set; }
        public string DisplayName { get; set; }

        public string City { get; set; }
    }
}
