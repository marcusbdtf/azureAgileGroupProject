using TeamKville.Shared.Dto;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
	public interface IGenreRepository
	{
		Task<List<GenreDto>> GetAll();
		Task<string> CreateGenre(string genre);
	}
}
