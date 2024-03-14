using Hope.Core.Common;
using Hope.Core.Features.PostOperation.Commands.PinPost;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hope.Core.Features.CommentOperation.Commands.UpdateComment
{
    internal class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Response>
    {
        private readonly IUnitofWork work;


        private readonly IStringLocalizer<PinPostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        public UpdateCommentCommandHandler(IUnitofWork work, IStringLocalizer<PinPostCommandHandler> localizer, UserManager<User> userManager)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
        }
        public async Task<Response> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"]);

            }

            var comment = user.Comments.FirstOrDefault(i => i.Id == request.CommentId);

            if (comment == null) return await Response.FailureAsync("You are not allowed to update this Comment");

            comment.Content = request.Content;

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);
        }
    }
}
