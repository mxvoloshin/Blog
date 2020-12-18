using System.Collections.Generic;
using Blog.Application.Common;

namespace Blog.Application.Posts.Queries.GetAllPostsQuery
{
    public class GetAllPostsQueryResult : PagedResult
    {
        public GetAllPostsQueryResult(ResultType status) : base(status)
        {
        }

        public IEnumerable<PostItemModel> Posts { get; set; }
    }
}