﻿using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.PostOperation.Commands.PinPost
{
    internal class PinPostCommandHandler : IRequestHandler<PinPostCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<PinPostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IValidator<PinPostCommand> validator;
        public PinPostCommandHandler(IUnitofWork work, IStringLocalizer<PinPostCommandHandler> localizer, UserManager<User> userManager, IValidator<PinPostCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.validator = validator;
        }
        public async Task<Response> Handle(PinPostCommand command, CancellationToken cancellationToken)
        {
            var validationresult = await validator.ValidateAsync(command);

            if (!validationresult.IsValid)
            {
                return await Response.FailureAsync(validationresult.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }
            if (command.IsPeople) 
                return await PinPost<PostOfLostPeople>(command.UserId,command.PostId);
            else
                return await PinPost<PostOfLostThings>(command.UserId,command.PostId);

        }
        private async Task<Response> PinPost<T>(string UserId, int PostId) where T : Post
        {
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {

                return await Response.FailureAsync(localizer["UserNotExist"].Value);

            }


            Post? post = await work.Repository<T>().GetItem(i => i.Id == PostId);
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"].Value);

            }
            if (typeof(T).Name == nameof(PostOfLostPeople))
            {

                user.PinningPeoples.Add((PostOfLostPeople)post);

            }
            else
            {

                user.PinningThings.Add((PostOfLostThings)post);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);
        }
    }
}
