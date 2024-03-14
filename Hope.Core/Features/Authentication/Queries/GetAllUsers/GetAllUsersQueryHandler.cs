using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.GetAllUsers
{
    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response>
    {
        private readonly IUnitofWork unitofWork;
        private readonly IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer;

        public GetAllUsersQueryHandler(IUnitofWork unitofWork, IStringLocalizer<GetAllPostsOfAccidentQueryHandler> localizer)
        {
            this.unitofWork = unitofWork;
            this.localizer = localizer;
        }

        public async Task<Response> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var user = unitofWork.Repository<User>().Get(i => i).Result.Select(i => new { i.DisplayName, i.Id, i.UserImage });

            return await Response.SuccessAsync(user, localizer["Success"]);
        }
    }
}
