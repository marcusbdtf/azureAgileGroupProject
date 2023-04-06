using TeamKville.Server.Data.DataModels;

namespace TeamKville.Server.Data.Repositories.Interfaces
	{
	public interface ICategoryRepository
		{
			
IEnumerable<Category> GetCategories();
			
	}
	}
