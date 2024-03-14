using Hope.Core.Common;
using Hope.Core.Dtos;
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
        private readonly IStringLocalizer<AuthService> localizer;
        private readonly IUnitofWork unitofWork;
        private readonly IMediaService mediaService;

        public GetProfileQueryHandler(UserManager<User> userManager, IMapper mapper, IStringLocalizer<AuthService> localizer, IConfiguration configuration, IUnitofWork unitofWork, IMediaService mediaService)
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

        
            List<PostDto> allposts = [..   user.lostPeople.Adapt<List<PostDto>>(), .. user.lostThings.Adapt<List<PostDto>>()];


            return await Response.SuccessAsync(new { user.Name, user.UserName, user.UserImage, user.City, allposts }, localizer["Success"]);

        }
    }
}
