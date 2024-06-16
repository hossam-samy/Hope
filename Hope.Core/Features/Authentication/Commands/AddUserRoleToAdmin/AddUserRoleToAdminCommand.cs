using Hope.Core.Common;
using MediatR;

namespace Hope.Core.Features.Authentication.Commands.AddUserRoleToAdmin
{
    public class AddUserRoleToAdminCommand:IRequest<Response>
    {
        public string UserId { get; set; }

    }
}
