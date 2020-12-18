using System;

namespace Blog.Application.Posts.Queries.GetAllPostsQuery
{
    public class PostItemModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime PostDate { get; set; }
    }
}