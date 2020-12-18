using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common;
using Blog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Queries.GetPostQuery
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, GetPostQueryResult>
    {
        private readonly BlogDbContext _dbContext;

        public GetPostQueryHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetPostQueryResult> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            // TODO: move hard coded ordering by CommentDate to controller level
            var post = await _dbContext.Posts
                                       .AsNoTracking()
                                       .Include(p => p.Comments
                                                      .OrderByDescending(c => c.CommentDate) //this will work only for EF Core 5
                                                      .Skip((request.CommentsPageNumber - 1) * request.CommentsPageSize)
                                                      .Take(request.CommentsPageSize))
                                       .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);
            if (post == null)
            {
                return new GetPostQueryResult(ResultType.SourceNotFound)
                {
                    Message = $"Post not found (id:{request.Id})"
                };
            }

            var totalCommentsCount = await _dbContext.Comments.CountAsync(x => x.PostId == post.Id, cancellationToken: cancellationToken);

            return new GetPostQueryResult(ResultType.Ok)
            {
                Title = post.Title,
                Text = post.Text,
                PostDate = post.PostDate,
                Comments = post.Comments.Select(x => new GetPostCommentItemModel
                {
                    Id = x.Id,
                    Text = x.Text,
                    CommentDate = x.CommentDate
                }),
                CommentsPageNumber = request.CommentsPageNumber,
                TotalCommentsPagesCount = (totalCommentsCount + request.CommentsPageSize - 1) / request.CommentsPageSize
            };
        }
    }
}