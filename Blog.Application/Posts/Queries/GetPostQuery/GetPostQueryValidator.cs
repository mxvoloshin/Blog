using FluentValidation;

namespace Blog.Application.Posts.Queries.GetPostQuery
{
    public class GetPostQueryValidator : AbstractValidator<GetPostQuery>
    {
        public GetPostQueryValidator()
        {
            RuleFor(x => x.CommentsPageNumber).GreaterThan(0);
            RuleFor(x => x.CommentsPageSize).GreaterThan(0);
        }
    }
}