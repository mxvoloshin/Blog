using Blog.Application.Common;
using MediatR;

namespace Blog.Application.Posts.Queries.GetAllPostsQuery
{
    public class GetAllPostsQuery : PagedQuery, IRequest<GetAllPostsQueryResult>
    {

    }
}