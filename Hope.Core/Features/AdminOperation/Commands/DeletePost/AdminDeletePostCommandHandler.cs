using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.AdminOperation.Commands.DeletePost
{
    internal class AdminDeletePostCommandHandler : IRequestHandler<AdminDeletePostCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<AdminDeletePostCommandHandler> localizer;
        private readonly IValidator<AdminDeletePostCommand> validator;
        public AdminDeletePostCommandHandler(IUnitofWork work, IStringLocalizer<AdminDeletePostCommandHandler> localizer, IValidator<AdminDeletePostCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.validator = validator;
        }

        public async Task<Response> Handle(AdminDeletePostCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

   
            Post? post;
            if (command.IsPeople)
            
              post = await work.Repository<PostOfLostPeople>().GetItem(i=>i.Id==command.PostId);
            
            else 
              post = await work.Repository<PostOfLostThings>().GetItem(i => i.Id == command.PostId);


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
