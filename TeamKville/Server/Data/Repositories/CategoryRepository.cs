using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;

namespace TeamKville.Server.Data.Repositories
	{
	public class CategoryRepository : ICategoryRepository
		{
			private readonly DataContext _context;

			public CategoryRepository(DataContext context)
			{
				_context = context;
			}

			public IEnumerable<Category> GetCategories()
			{
				return _context.Categories.ToList();
			}
		}
	}
