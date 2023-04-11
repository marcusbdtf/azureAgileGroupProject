using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
	public interface ICommentRepository
	{
		Task<List<CommentDto>> GetCommentsByProductId(int productId);
		Task<string> CreateComment(CreateCommentModel newCommentInput);
	}
}
