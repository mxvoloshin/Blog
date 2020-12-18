using System;
using Blog.Application.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebAPI.Controllers
{
    [AllowAnonymous] //TODO: remove in production
    [ApiController]
    [Route("api/[controller]")]
    public class BlogBaseController : ControllerBase
    {
        protected IActionResult ApiResponse(BaseResult result)
        {
            switch (result.Status)
            {
                case ResultType.Unknown:
                case ResultType.Failed:
                    return BadRequest(new { result.Message });
                case ResultType.SourceNotFound:
                    return NotFound(new { result.Message });
                case ResultType.Ok:
                    return Ok(result);
                case ResultType.SourceDeleted:
                    return NoContent();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected IActionResult CreatedApiResponse(BaseResult result, string actionName, object routeValues)
        {
            if (result.Status == ResultType.SourceCreated)
            {
                return CreatedAtAction(actionName, routeValues, null);
            }

            return ApiResponse(result);
        }
    }
}