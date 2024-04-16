using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.DeletePost
{
    internal class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<DeletePostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IValidator<DeletePostCommand> validator;
        public DeletePostCommandHandler(IUnitofWork work, IStringLocalizer<DeletePostCommandHandler> localizer, UserManager<User> userManager, IValidator<DeletePostCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
        }

        public async Task<Response> Handle(DeletePostCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var user = await userManager.FindByIdAsync(command.UserId!);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"].Value);
            }

           

            Post? post;
            if (command.IsPeople)
            
              post = user?.lostPeople?.FirstOrDefault(i=>i.Id==command.PostId);
            
            else 
              post = user?.lostThings?.FirstOrDefault(i => i.Id == command.PostId);
            

                if (post == null)
                {
                    return await Response.FailureAsync(localizer["BlockDeletingPost"].Value);

                }
                post.IsDeleted = true;

            await work.SaveAsync();
                
                return await Response.SuccessAsync(localizer["Success"].Value);
            }

        }
}
