﻿using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.HidePosts
{
    internal class HidePostHandler : IRequestHandler<HidePostsCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<HidePostHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IValidator<HidePostsCommand> validator;
        private readonly IHttpContextAccessor accessor;
        public HidePostHandler(IUnitofWork work, IStringLocalizer<HidePostHandler> localizer, UserManager<User> userManager, IValidator<HidePostsCommand> validator, IHttpContextAccessor accessor)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
            this.accessor = accessor;
        }
        public async Task<Response> Handle(HidePostsCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            var userid = accessor?.HttpContext?.User.Claims.FirstOrDefault(i => i.Type == "uid")?.Value;

            if (command.IsPeople)
                return await HidePost<PostOfLostPeople>(userid!, command.PostId);
            else
                return await HidePost<PostOfLostThings>(userid!, command.PostId);

        }
        public async Task<Response> HidePost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
                return await Response.FailureAsync(localizer["UserNotExist"].Value);


            Post? post =  await work.Repository<T>().GetItem(i => i.Id == PostId);
            

            if (typeof(T).Name == nameof(PostOfLostPeople))
            {
                if(user.PinningPeoples.Any(i => i == post))
                   return await Response.FailureAsync("HideError");

                user.HiddingPeoples.Add((PostOfLostPeople)post);

            }
            else
            {
                if (user.PinningThings.Any(i => i == post))
                    return await Response.FailureAsync("HideError");

                user.HiddingThings.Add((PostOfLostThings)post);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}