using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Data.Repositories
	{
	public class CategoryRepository : ICategoryRepository
		{
			private readonly DataContext _context;

			public CategoryRepository(DataContext context)
			{
				_context = context;
			}

			public async Task<IEnumerable<CategoryDto>> GetCategories()
			{
				return await _context.Categories.Select(x => new CategoryDto
				{
					CategoryId = x.CategoryId,
					Name = x.Name
				}).ToListAsync();
			}
		}
	}
