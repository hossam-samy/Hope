using Hope.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Commands.AddUserImage
{
    public class AddUserImageCommand:IRequest<Response>
    {

        public string? UserId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
