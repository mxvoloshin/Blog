using Blog.Application.Common;

namespace Blog.Application.Posts.Commands.DeletePostCommand
{
    public class DeletePostCommandResult : BaseResult
    {
        public DeletePostCommandResult(ResultType status) : base(status)
        {
        }
    }
}