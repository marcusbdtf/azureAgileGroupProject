using TeamKville.Server.Data.DataModels;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
	public interface ICategoryRepository
	{
		Task<IEnumerable<CategoryDto>> GetCategories();
	}
}
