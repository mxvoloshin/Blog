using FluentValidation;

namespace Blog.Application.Posts.Queries.GetAllPostsQuery
{
    public class GetAllPostsQueryValidator : AbstractValidator<GetAllPostsQuery>
    {
        public GetAllPostsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThan(0);
            RuleFor(x => x.PageSize).GreaterThan(0);
        }
    }
}