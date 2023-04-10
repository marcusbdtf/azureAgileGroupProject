using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly DataContext _dbContext;

		public CommentRepository(DataContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<List<CommentDto>> GetCommentsByProductId(int productId)
		{
			var comments = await _dbContext.Comments
				.Where(x => x.ProductId == productId)
				.Select(x => new CommentDto
				{
					CommentId = x.CommentId,
					Name = x.Name,
					Text = x.Text,
					Rating = x.Rating,
					Date = x.Date
				})
				.ToListAsync();

			return comments;
		}

		public Task<string> CreateComment(CreateCommentModel newCommentInput)
		{
			var newComment = new Comment
			{
				Name = newCommentInput.Name,
				Text = newCommentInput.Text,
				Rating = newCommentInput.Rating,
				Date = DateTime.Now,
				ProductId = newCommentInput.ProductId,
			};
			_dbContext.Comments.Add(newComment);
			_dbContext.SaveChanges();

			return Task.FromResult("Comment successfully created");

		}
	}
}
