using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;


namespace Hope.Core.Features.PostOperation.Commands.CreatePostForPeople
{
    internal class CreatePostForPeopleCommandHandler : IRequestHandler<CreatePostForPeopleCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<CreatePostForPeopleCommandHandler> localizer;
        private readonly IMediaService mediaService;
        private readonly IValidator<CreatePostForPeopleCommand> validator;
        private readonly IHttpContextAccessor accessor;
        private readonly IRecommendationService recommendationService;

        public CreatePostForPeopleCommandHandler(IUnitofWork work, IMapper mapper, IStringLocalizer<CreatePostForPeopleCommandHandler> localizer, IMediaService mediaService, UserManager<User> userManager, IValidator<CreatePostForPeopleCommand> validator, IHttpContextAccessor accessor, IRecommendationService recommendationService)
        {
            this.work = work;
            this.localizer = localizer;
            this.mediaService = mediaService;
            this.validator = validator;
            this.accessor = accessor;
            this.recommendationService = recommendationService;
        }
        public async Task<Response> Handle(CreatePostForPeopleCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var post = command.Adapt<PostOfLostPeople>();
            
            post.UserId= accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;
            
            await work.Repository<PostOfLostPeople>().AddAsync(post);

             post.ImageUrl = await mediaService.AddFileAsync(command.ImageFile, post.GetType().Name, post.Id.ToString());

            DateTime.TryParse(command.MissigDate, out DateTime missingDate);
            post.MissigDate = missingDate;

            var location=await work.Repository<Location>().GetItem(i=>i.City==command.City); 

            post.Cluster =await  recommendationService.predict(location.Longitude, location.Latitude);

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
