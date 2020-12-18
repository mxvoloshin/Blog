using Blog.Application.Validators;
using FluentValidation;

namespace Blog.Application.Comments.Commands.CreateCommentCommand
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(x => x.Text).NotEmpty(); // TODO: max length?
            RuleFor(x => x.CommentDate).Cascade(CascadeMode.Stop)
                                       .NotNull()
                                       .IsNotFutureDate();
        }
    }
}