using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common;
using Blog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Commands.DeletePostCommand
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, DeletePostCommandResult>
    {
        private readonly BlogDbContext _dbContext;

        public DeletePostCommandHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DeletePostCommandResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            // 3. Deleting a post (only if it has no comments)
            // This validation better be done by settings onDelete: ReferentialAction.Restrict instead of Cascade
            var result = await _dbContext.Posts
                                         .Select(x => new
                                         {
                                             Post = x,
                                             CommentsCount = x.Comments.Count()
                                         })
                                         .FirstOrDefaultAsync(x => x.Post.Id == request.PostId, cancellationToken);
            if (result == null)
            {
                return new DeletePostCommandResult(ResultType.SourceNotFound)
                {
                    Message = $"Post not found (id:{request.PostId})"
                };
            }

            if (result.CommentsCount > 0)
            {
                return new DeletePostCommandResult(ResultType.Failed)
                {
                    Message = $"Cannot delete Post that has Comments (id:{request.PostId})"
                };
            }

            // TODO: SoftDeletion is preferable over hard deleting
            _dbContext.Posts.Remove(result.Post);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new DeletePostCommandResult(ResultType.SourceDeleted);
        }
    }
}