using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
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

        public CreatePostForThingsCommandHandler(IUnitofWork work, IStringLocalizer<CreatePostForThingsCommandHandler> localizer, IMediaService mediaService, IValidator<CreatePostForThingsCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.mediaService = mediaService;
            this.validator = validator;
        }
        public async Task<Response> Handle(CreatePostForThingsCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var post = command.Adapt<PostOfLostThings>();
            await work.Repository<PostOfLostThings>().AddAsync(post);

            post.ImageUrl = await mediaService.AddFileAsync(command.ImageFile, post.GetType().Name, post.Id.ToString());

            await work.Repository<PostOfLostThings>().Update(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
