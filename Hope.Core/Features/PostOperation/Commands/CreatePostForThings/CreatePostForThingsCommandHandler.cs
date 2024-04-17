using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.CreatePostForThings
{
    internal class CreatePostForThingsCommandHandler : IRequestHandler<CreatePostForThingsCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<CreatePostForThingsCommandHandler> localizer;
        private readonly IMediaService mediaService;
        private readonly IValidator<CreatePostForThingsCommand> validator;
        private readonly IHttpContextAccessor accessor;

        public CreatePostForThingsCommandHandler(IUnitofWork work, IStringLocalizer<CreatePostForThingsCommandHandler> localizer, IMediaService mediaService, IValidator<CreatePostForThingsCommand> validator, IHttpContextAccessor accessor)
        {
            this.work = work;
            this.localizer = localizer;
            this.mediaService = mediaService;
            this.validator = validator;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(CreatePostForThingsCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var post = command.Adapt<PostOfLostThings>();
            post.UserId = accessor?.HttpContext?.User?.Claims?.FirstOrDefault(i => i.Type == "uid")?.Value!;

            await work.Repository<PostOfLostThings>().AddAsync(post);

            post.ImageUrl = await mediaService.AddFileAsync(command.ImageFile, post.GetType().Name, post.Id.ToString());

           
            DateTime.TryParse(command.MissigDate,out DateTime missingDate);
            post.MissigDate = missingDate;
            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
