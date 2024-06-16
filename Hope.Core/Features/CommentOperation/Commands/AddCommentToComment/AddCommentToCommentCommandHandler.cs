using FluentValidation;
using Hope.Core.Common;
using Hope.Core.Interfaces;
using Hope.Domain.Model;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace Hope.Core.Features.CommentOperation.Commands.AddCommentToComment
{
    internal class AddCommentToCommentCommandHandler : IRequestHandler<AddCommentToCommentCommand, Response>
    {
        private readonly IUnitofWork work;
        private readonly IStringLocalizer<AddCommentToCommentCommandHandler> localizer;
        private readonly IMapper mapper;
        private readonly IValidator<AddCommentToCommentCommand> validator;
        private readonly IHttpContextAccessor accessor;


        public AddCommentToCommentCommandHandler(IMapper mapper, IStringLocalizer<AddCommentToCommentCommandHandler> localizer, IUnitofWork work, IValidator<AddCommentToCommentCommand> validator, IHttpContextAccessor accessor)
        {
            this.mapper = mapper;
            this.localizer = localizer;
            this.work = work;
            this.validator = validator;
            this.accessor = accessor;
        }

        public async Task<Response> Handle(AddCommentToCommentCommand command, CancellationToken cancellationToken)
        {
            var result=await validator.ValidateAsync(command);

            if(!result.IsValid)
            {
                return await Response.FailureAsync(result.Errors.Select(i => i.ErrorMessage), localizer["Faild"].Value);
            }

            Comment? comment =  await work.Repository<Comment>().GetItem(i => i.Id == command.CommentId);
            if (comment == null)
            {
                return await Response.FailureAsync(localizer["CommentNotExist"].Value);
            }

            var newcomment = mapper.Map<Comment>(command);
            
            newcomment.UserId = accessor.HttpContext!.User.FindFirst("uid")!.Value;
            
            comment.Comments.Add(newcomment);

            await work.SaveAsync();

            return await Response.SuccessAsync(localizer["Success"].Value);

        }
    }
}
