using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.Authentication.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response>
    {
        private readonly IUnitofWork unitofWork;

        public GetAllUsersQueryHandler(IUnitofWork unitofWork)
        {
            this.unitofWork = unitofWork;
        }

        public async Task<Response> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var user = unitofWork.Repository<User>().Get(i => i).Result.Select(i => new { i.Name, i.Id, i.UserImage });

            return await Response.SuccessAsync(user);
        }
    }
}
