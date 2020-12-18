using System;
using MediatR;

namespace Blog.Application.Comments.Commands.CreateCommentCommand
{
    public class CreateCommentCommand : IRequest<CreateCommentCommandResult>
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public DateTime? CommentDate { get; set; }
    }
}