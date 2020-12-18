using System.Threading;
using System.Threading.Tasks;
using Blog.Application.Common;
using Blog.Domain;
using Blog.Infrastructure;
using MediatR;

namespace Blog.Application.Posts.Commands.CreatePostCommand
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostCommandResult>
    {
        private readonly BlogDbContext _dbContext;

        public CreatePostCommandHandler(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreatePostCommandResult> Handle(CreatePostCommand request,
                                                          CancellationToken cancellationToken)
        {
            var post = await _dbContext.Posts.AddAsync(new Post
                {
                    Title = request.Title,
                    Text = request.Text,
                    PostDate = request.PostDate.Value
                },
                cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CreatePostCommandResult(ResultType.SourceCreated)
            {
                Id = post.Entity.Id
            };
        }
    }
}