using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.AdminOperation.Queries.GetAllUsers
{
    internal class AdminGetAllUsersQueryHandler : IRequestHandler<AdminGetAllUsersQuery, Response>
    {
        private readonly IUnitofWork unitofWork;
        private readonly IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer;

        public AdminGetAllUsersQueryHandler(IUnitofWork unitofWork, IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer)
        {
            this.unitofWork = unitofWork;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(AdminGetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var user = unitofWork.Repository<User>().Get(i => i).Result.Select(i => new { i.DisplayName, i.Email, i.UserImage });

            return await Response.SuccessAsync(user, localizer["Success"].Value);
        }
    }
}
