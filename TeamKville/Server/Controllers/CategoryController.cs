using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;

namespace TeamKville.Server.Controllers
	{

		[Route("[controller]")]
			[ApiController]
			public class CategoryController : ControllerBase
			{
				private readonly ICategoryRepository _categoryRepository;

				public CategoryController(ICategoryRepository repo)
				{
					_categoryRepository = repo;
				}

				[HttpGet("all")]
				[ProducesResponseType(StatusCodes.Status200OK)]
				[ProducesResponseType(StatusCodes.Status404NotFound)]

				public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
				{
					var categories = _categoryRepository.GetCategories();
					return Ok(categories);
				}

			}
		}
	
