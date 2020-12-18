using MediatR;

namespace Blog.Application.Posts.Queries.GetPostQuery
{
    public class GetPostQuery : IRequest<GetPostQueryResult>
    {
        public int Id { get; set; }
        public int CommentsPageNumber { get; set; }
        public int CommentsPageSize { get; set; }
    }
}