using System;

namespace Blog.Domain
{
    public class Comment
    {
        public int Id { get; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostId { get; }
        public Post Post { get; }
    }
}