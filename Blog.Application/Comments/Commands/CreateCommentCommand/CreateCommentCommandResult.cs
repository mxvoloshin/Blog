using Blog.Application.Common;

namespace Blog.Application.Comments.Commands.CreateCommentCommand
{
    public class CreateCommentCommandResult : BaseResult
    {
        public CreateCommentCommandResult(ResultType status) : base(status)
        {
        }
    }
}