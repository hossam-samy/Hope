using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToPost
{
    internal class AddCommentToPostCommandHandler : IRequestHandler<AddCommentToPostCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<AddCommentToPostCommandHandler> localizer;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IValidator<AddCommentToPostCommand> validator;
        public AddCommentToPostCommandHandler(IUnitofWork work, IStringLocalizer<AddCommentToPostCommandHandler> localizer, UserManager<User> userManager, IMapper mapper, IValidator<AddCommentToPostCommand> validator)
        {
            this.work = work;
            this.localizer = localizer;
            this.userManager = userManager;
            this.mapper = mapper;
            this.validator = validator;
        }
        public async Task<Response> Handle(AddCommentToPostCommand command, CancellationToken cancellationToken)
        {
            var result = await validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"]);
            }

            if (command.IsPeople)
                return await AddCommentToPost<PostOfLostPeople>(command);
            else
                return await AddCommentToPost<PostOfLostThings>(command);
        }
        private async Task<Response> AddCommentToPost<T>(AddCommentToPostCommand command) where T : Post
        {


            Post? post = work.Repository<T>().Get(i => i.Id == command.PostId).Result.FirstOrDefault();
            if (post == null)
            {

                return await Response.FailureAsync(localizer["PostNotExist"]);

            }

            var commnet = mapper.Map<Comment>(command);

            if (typeof(T).Name == nameof(PostOfLostPeople))
            {


                commnet.Peopleid = command.PostId;

                var people = (PostOfLostPeople)post;

                people.Comments.Add(commnet);


            }
            else
            {
                commnet.Thingsid = command.PostId;

                var Thing = (PostOfLostThings)post;

                Thing.Comments.Add(commnet);

            }

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"]);

        }
    }
}
