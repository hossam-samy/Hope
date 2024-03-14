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
       
        private readonly IStringLocalizer<PostService> localizer;
        private readonly IMediaService mediaService;
        public CreatePostForThingsCommandHandler(IUnitofWork work, IStringLocalizer<PostService> localizer, IMediaService mediaService)
        {
            this.work = work;
            this.localizer = localizer;
            this.mediaService = mediaService;
            
        }
        public async Task<Response> Handle(CreatePostForThingsCommand command, CancellationToken cancellationToken)
        {
            var post = command.Adapt<PostOfLostThings>();
            await work.Repository<PostOfLostThings>().AddAsync(post);
            var result = await mediaService.AddFileAsync(command.ImageFile, post.GetType().Name, post.Id.ToString());

            post.ImageUrl = result;

            await work.Repository<PostOfLostThings>().Update(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
