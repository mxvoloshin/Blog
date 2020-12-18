using MediatR;

namespace Blog.Application.Posts.Commands.DeletePostCommand
{
    public class DeletePostCommand : IRequest<DeletePostCommandResult>
    {
        public int PostId { get; set; }
    }
}