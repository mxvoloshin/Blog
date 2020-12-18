using System;
using System.Collections.Generic;

namespace Blog.Domain
{
    public class Post
    {
        private HashSet<Comment> _comments;

        public int Id { get; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime PostDate { get; set; }

        public IEnumerable<Comment> Comments => _comments;

        public void AddComment(Comment comment)
        {
            if (_comments == null)
            {
                _comments = new HashSet<Comment>();
            }

            _comments.Add(comment);
        }
    }
}