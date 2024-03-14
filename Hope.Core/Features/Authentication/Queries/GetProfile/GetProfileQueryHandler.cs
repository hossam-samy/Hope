using Hope.Core.Common;
using Hope.Core.Dtos;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.Authentication.Queries.GetProfile
{
    internal class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Response>
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<GetProfileQueryHandler> localizer;
        private readonly IUnitofWork unitofWork;
        private readonly IMediaService mediaService;

        public GetProfileQueryHandler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<GetProfileQueryHandler> localizer, IConfiguration configuration, IUnitofWork unitofWork, IMediaService mediaService)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.localizer = localizer;
            this.configuration = configuration;
            this.unitofWork = unitofWork;
            this.mediaService = mediaService;
        }
        public async Task<Response> Handle(GetProfileQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(query.UserId);

            if (user == null)
            {
                return await Response.FailureAsync(localizer["UserNotExist"]);
            }

        
            List<GetAllPostsOfPeopleQueryResponse> allposts = [..   user.lostPeople.Adapt<List<GetAllPostsOfPeopleQueryResponse>>(), .. user.lostThings.Adapt<List<GetAllPostsOfPeopleQueryResponse>>()];


            return await Response.SuccessAsync(new { user.DisplayName, user.UserName, user.UserImage, user.City, allposts }, localizer["Success"]);

        }
    }
}
