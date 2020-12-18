using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common;
using Blog.Domain;
using Blog.Infrastructure;
using MediatR;

namespace Blog.Application.Comments.Commands.CreateCommentCommand
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentCommandResult>
    {
        private readonly BlogDbContext _dbContext;

        public CreateCommentCommandHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateCommentCommandResult> Handle(CreateCommentCommand request,
                                                       CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.FindAsync(request.PostId);
            if (post == null)
            {
                return new CreateCommentCommandResult(ResultType.SourceNotFound)
                {
                    Message = $"Post not found (id:{request.PostId})"
                };
            }

            post.AddComment(new Comment
            {
                Text = request.Text,
                CommentDate = request.CommentDate.Value
            });

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreateCommentCommandResult(ResultType.SourceCreated)
            {
                Id = post.Comments.First().Id
            };
        }
    }
}