using Hope.Core.Common;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.AdminOperation.Queries.GetAdminsCount
{
    public class GetAdminsCountQuery:IRequest<Response>
    {
    }

    public class GetAdminsCountQueryHandler : IRequestHandler<GetAdminsCountQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IStringLocalizer<GetAdminsCountQueryHandler> localizer;

        public GetAdminsCountQueryHandler(UserManager<User> userManager, IStringLocalizer<GetAdminsCountQueryHandler> localizer)
        {
            this.userManager = userManager;
            this.localizer = localizer;
        }


        public async Task<Response> Handle(GetAdminsCountQuery query, CancellationToken cancellationToken)
        {

            var adminsCount =  userManager.GetUsersInRoleAsync("Admin").Result.Count();

            return await Response.SuccessAsync(adminsCount, localizer["Success"]);
            
        }
    }
}
