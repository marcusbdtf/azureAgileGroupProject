using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

		[HttpGet("{productId:int}", Name = "GetCommentsProductById")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<List<CommentDto>>> GetCommentsProductById(int productId)
		{
			var data = await _commentRepository.GetCommentsByProductId(productId);
			return Ok(data);
		}

		[HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateComment(CreateCommentModel newComment)
        {
            var comment = await _commentRepository.CreateComment(newComment);
            return Ok(comment);
        }
    }

}
