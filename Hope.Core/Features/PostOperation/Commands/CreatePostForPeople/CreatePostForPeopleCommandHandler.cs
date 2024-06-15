using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Queries.GetAllPostsOfAccidents;
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
        private readonly IFaceRecognitionService faceRecognitionService;

        public CreatePostForPeopleCommandHandler(IUnitofWork work, IMapper mapper, IStringLocalizer<CreatePostForPeopleCommandHandler> localizer, IMediaService mediaService, UserManager<User> userManager, IValidator<CreatePostForPeopleCommand> validator, IHttpContextAccessor accessor, IRecommendationService recommendationService, IFaceRecognitionService faceRecognitionService)
        {
            this.work = work;
            this.localizer = localizer;
            this.mediaService = mediaService;
            this.validator = validator;
            this.accessor = accessor;
            this.recommendationService = recommendationService;
            this.faceRecognitionService = faceRecognitionService;
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

            var location = await work.Repository<Location>().GetItem(i => i.City == command.City);

            var res = await recommendationService.predict(location.Longitude, location.Latitude);

            if (res.IsSuccess)
                post.Cluster = int.Parse(res.Data.ToString());
            else return res;



            var faceRecRes = await faceRecognitionService.predict(Path.GetFullPath($"wwwroot/PostOfLostPeopleImages/{post.Id}.jpg"));

            if (!faceRecRes.IsSuccess)
                return faceRecRes;

            PostOfLostPeople? matchPost = null;

          if(faceRecRes.Data.ToString()!= post.Id.ToString())
            matchPost = await work.Repository<PostOfLostPeople>().GetItem(i => i.ImageUrl.Contains(faceRecRes.Data.ToString()));
            
            await work.SaveAsync();

            return await Response.SuccessAsync(matchPost,localizer["Success"].Value);
        }
    }
}
