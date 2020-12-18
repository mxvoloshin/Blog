using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common;
using Blog.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Application.Posts.Queries.GetAllPostsQuery
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, GetAllPostsQueryResult>
    {
        private readonly BlogDbContext _dbContext;

        public GetAllPostsQueryHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetAllPostsQueryResult> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            // TODO: move hard coded ordering by PostDate to controller level
            var posts = await _dbContext.Posts
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.PostDate)
                                        .Skip((request.PageNumber - 1) * request.PageSize)
                                        .Take(request.PageSize)
                                        .Select(x => new PostItemModel
                                        {
                                            Title = x.Title,
                                            Text = x.Text,
                                            PostDate = x.PostDate
                                        }).ToListAsync(cancellationToken);

            var totalCount = await _dbContext.Posts.CountAsync(cancellationToken: cancellationToken);

            return new GetAllPostsQueryResult(ResultType.Ok)
            {
                Posts = posts,
                PageNumber = request.PageNumber,
                PagesCount = (totalCount + request.PageSize - 1) / request.PageSize
            };
        }
    }
}