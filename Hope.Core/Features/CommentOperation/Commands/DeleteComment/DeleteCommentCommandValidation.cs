using FluentValidation;

namespace Hope.Core.Features.CommentOperation.Commands.DeleteComment
{
    public class DeleteCommentCommandValidation:AbstractValidator<DeleteCommentCommand> 
    {
        public DeleteCommentCommandValidation()
        {
            //RuleFor(i => i.UserId).NotNull().WithMessage("PostId is Required").NotEmpty().WithMessage("PostId is Required");

            RuleFor(i => i.CommentId).NotEmpty().WithMessage("CommentId is Required");
        }
    }
}
