using Blog.Application.Validators;
using FluentValidation;

namespace Blog.Application.Posts.Commands.CreatePostCommand
{
    public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
    {
        public CreatePostCommandValidator()
        {
            RuleFor(v => v.Title).NotEmpty(); // TODO: max length?
            RuleFor(v => v.Text).NotEmpty(); // TODO: max length?
            RuleFor(v => v.PostDate).Cascade(CascadeMode.Stop)
                                    .NotNull()
                                    .IsNotFutureDate();
        }
    }
}