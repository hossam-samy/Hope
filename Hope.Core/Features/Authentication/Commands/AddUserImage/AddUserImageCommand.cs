using Hope.Core.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Hope.Core.Features.Authentication.Commands.AddUserImage
{
    public class AddUserImageCommand:IRequest<Response>
    {
        public IFormFile? Image { get; set; }
    }
}
