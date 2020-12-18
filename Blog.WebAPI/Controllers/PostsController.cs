using System;
using System.Threading.Tasks;
using Blog.Application.Comments.Commands.CreateCommentCommand;
using Blog.Application.Posts.Commands.CreatePostCommand;
using Blog.Application.Posts.Commands.DeletePostCommand;
using Blog.Application.Posts.Queries.GetAllPostsQuery;
using Blog.Application.Posts.Queries.GetPostQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    public class PostsController : BlogBaseController
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var query = new GetAllPostsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await _mediator.Send(query);
            return ApiResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, int commentsPageNumber, int commentsPageSize)
        {
            var query = new GetPostQuery
                { Id = id, CommentsPageNumber = commentsPageNumber, CommentsPageSize = commentsPageSize };
            var result = await _mediator.Send(query);
            return ApiResponse(result);
        }

        [HttpGet("{id}/comments/{commentId}")]
        public Task<IActionResult> GetComment(int id, int commentId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(CreatePostCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedApiResponse(result, nameof(Get), new { result.Id });
        }

        [HttpPost("{id}/comments")]
        public async Task<IActionResult> CreateComment(int id, CreateCommentCommand command)
        {
            command.PostId = id;
            var result = await _mediator.Send(command);
            return CreatedApiResponse(result, nameof(GetComment), new { id, commentId = result.Id });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeletePostCommand { PostId = id });
            return ApiResponse(result);
        }
    }
}