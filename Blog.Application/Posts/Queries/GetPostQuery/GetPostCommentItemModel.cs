using System;

namespace Blog.Application.Posts.Queries.GetPostQuery
{
    public class GetPostCommentItemModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
    }
}