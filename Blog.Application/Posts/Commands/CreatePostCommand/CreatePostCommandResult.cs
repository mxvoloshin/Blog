using Blog.Application.Common;

namespace Blog.Application.Posts.Commands.CreatePostCommand
{
    public class CreatePostCommandResult : BaseResult
    {
        public CreatePostCommandResult(ResultType status) : base(status)
        {
        }
    }
}