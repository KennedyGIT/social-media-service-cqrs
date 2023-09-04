using core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using post.common.DTOs;
using post.query.api.DTOs;
using post.query.api.Queries;
using post.query.domain.Entities;

namespace post.query.api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostLookupController : ControllerBase
    {
        private readonly ILogger<PostLookupController> _logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher;

        public PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher)
        {
            _logger = logger;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllPostsAsync() 
        {
            try 
            {
                var posts = await _queryDispatcher.SendAsync(new FindAllPostsQuery());
                return NormalResponse(posts);
            }
            catch (Exception ex) 
            {
                return ErrorResponse("Error while processing request to retrieve all posts!", ex);
            }
        }

        [HttpGet("byId/{postId}")]
        public async Task<ActionResult> GetByPostIdAsync(Guid postId) 
        {
            try 
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostByIdQuery { id = postId });
                return NormalResponse(posts);
            }
            catch (Exception ex) 
            {
                return ErrorResponse("Error while processing request to post by Id", ex);
            }
           
        }

        [HttpGet("byAuthor/{author}")]
        public async Task<ActionResult> GetByPostAuthorAsync(string author)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsByAuthorQuery { Author = author });
                return NormalResponse(posts);
            }
            catch (Exception ex)
            {
                return ErrorResponse("Error while processing request to find posts by author!", ex);
            }

        }

        [HttpGet("withComments")]
        public async Task<ActionResult> GetPostsWithComments() 
        {
            try 
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsWithCommentsQuery());
                return NormalResponse(posts);
            }
            catch(Exception ex) 
            {
                return ErrorResponse("Error while processing request to find post with comments", ex);
            }
        }

        [HttpGet("withLikes/{numberOfLikes}")]
        public async Task<ActionResult> GetPostsWithComments(int numberOfLikes)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsWithLikesQuery { NumberOfLikes = numberOfLikes });
                return NormalResponse(posts);
            }
            catch (Exception ex)
            {
                return ErrorResponse("Error while processing request to find post with likes", ex);
            }
        }

        private ActionResult ErrorResponse(string SAFE_ERROR_MESSAGE, Exception ex)
        {
            _logger.LogError(ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
            {
                Message = SAFE_ERROR_MESSAGE,
            });
        }

        private ActionResult NormalResponse(List<PostEntity> posts)
        {
            if (posts == null || !posts.Any())
                return NoContent();

            var count = posts.Count;
            return Ok(new PostLookupResponse
            {
                Posts = posts,
                Message = $"Successfully returned {count} post{(count > 1 ? "s" : string.Empty)}!"
            });
        }
    }
}
