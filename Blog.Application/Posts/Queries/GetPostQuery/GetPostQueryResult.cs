using System;
using System.Collections.Generic;
using Blog.Application.Common;

namespace Blog.Application.Posts.Queries.GetPostQuery
{
    public class GetPostQueryResult : BaseResult
    {
        public GetPostQueryResult(ResultType resultType) : base(resultType)
        {

        }

        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
        public IEnumerable<GetPostCommentItemModel> Comments { get; set; }
        public int CommentsPageNumber { get; set; }
        public int TotalCommentsPagesCount { get; set; }
    }
}