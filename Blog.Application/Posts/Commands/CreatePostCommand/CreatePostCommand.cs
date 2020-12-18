using System;
using MediatR;

namespace Blog.Application.Posts.Commands.CreatePostCommand
{
    public class CreatePostCommand : IRequest<CreatePostCommandResult>
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime? PostDate { get; set; }
    }
}