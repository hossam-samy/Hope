using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.UnHidePost
{
    public class UnHidePostCommandHandler:IRequestHandler<UnHidePostCommand,Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<UnHidePostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IValidator<UnHidePostCommand> validator;
        private readonly IHttpContextAccessor accessor;
        public UnHidePostCommandHandler(IUnitofWork work, IStringLocalizer<UnHidePostCommandHandler> localizer, UserManager<User> userManager, IValidator<UnHidePostCommand> validator, IHttpContextAccessor accessor)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
            this.accessor = accessor;
        }

        public async Task<Response> Handle(UnHidePostCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;

            if (command.IsPeople)
                return await UnHidePost<PostOfLostPeople>(userid!, command.PostId);
            else
                return await UnHidePost<PostOfLostThings>(userid!, command.PostId);

        }
        public async Task<Response> UnHidePost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"].Value);


            Post? post = await work.Repository<T>().GetItem(i => i.Id == PostId);
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"].Value);

            }
          
            if (user.HiddingPeoples.Any(i => i.Id == PostId))
            {
                user.HiddingPeoples.Remove((PostOfLostPeople)post);
            }
            
            else if(user.HiddingThings.Any(i => i.Id == PostId))
            {
                user.HiddingThings.Remove((PostOfLostThings)post);
            }
            else
            {
                return await Response.FailureAsync("UnHideError");
            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
