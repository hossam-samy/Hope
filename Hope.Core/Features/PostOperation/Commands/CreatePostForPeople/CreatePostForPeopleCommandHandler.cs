using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Core.Service;
using Hope.Domain.Model;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;


namespace Hope.Core.Features.PostOperation.Commands.CreatePostForPeople
{
    internal class CreatePostForPeopleCommandHandler : IRequestHandler<CreatePostForPeopleCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<CreatePostForPeopleCommandHandler> localizer;
        private readonly IMediaService mediaService;
        
        public CreatePostForPeopleCommandHandler(IUnitofWork work, IMapper mapper, IStringLocalizer<CreatePostForPeopleCommandHandler> localizer, IMediaService mediaService, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.mediaService = mediaService;
        }
        public async Task<Response> Handle(CreatePostForPeopleCommand command, CancellationToken cancellationToken)
        {
            var post = command.Adapt<PostOfLostPeople>();
            await work.Repository<PostOfLostPeople>().AddAsync(post);
            var result = await mediaService.AddFileAsync(command.ImageFile, post.GetType().Name, post.Id.ToString());

            post.ImageUrl = result;

            await work.Repository<PostOfLostPeople>().Update(post);

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
