using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Data.Repositories
{
	public class GenreRepository : IGenreRepository
	{

		private readonly DataContext _context;

		public GenreRepository(DataContext context)
		{
			_context = context;
		}

		public async Task<List<GenreDto>> GetAll()
		{
			return await _context.Genres.Select(x => new GenreDto
			{
				GenreId = x.GenreId,
				Name = x.Name
			}).ToListAsync();
		}

		public Task<string> CreateGenre(string genre)
		{
			var newGenre = new Genre
			{
				Name = genre,
			};

			_context.Genres.Add(newGenre);
			_context.SaveChanges();

			return Task.FromResult("Genre successfully created!");

		}
	}
}
