using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Controllers
{
    //[Route["api/[controller]")]
    [Route("api/genre")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
	        _genreRepository = genreRepository;
        }

        //Hämta alla genre
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll()
        {
            var data = await _genreRepository.GetAll();

            return Ok(data);
        }

        //Skapar genre
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateGenre(string genre)
        {
	        var newGenre = await _genreRepository.CreateGenre(genre);
	        return Ok(newGenre);
        }
	}
}
