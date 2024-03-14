using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToComment
{
    internal class AddCommentToCommentCommandHandler : IRequestHandler<AddCommentToCommentCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<AddCommentToCommentCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;

        public AddCommentToCommentCommandHandler(IMapper mapper, UserManager<User> userManager, IStringLocalizer<AddCommentToCommentCommandHandler> localizer, IUnitofWork work)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.localizer = localizer;
            this.work = work;
        }

        public async Task<Response> Handle(AddCommentToCommentCommand command, CancellationToken cancellationToken)
        {
            Comment? comment = work.Repository<Comment>().Get(i => i.Id == command.CommentId).Result.FirstOrDefault();
            if (comment == null)
            {

                return await Response.FailureAsync(localizer["CommentNotExist"]);
            }

            var newcomment = mapper.Map<Comment>(command);

            comment.Comments.Add(newcomment);

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);

        }
    }
}
