using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Controllers
{
    //[Route["api/[controller]")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Hämta alla users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var data = await _userRepository.GetAllUsers();
            

            return Ok(data.Value);
        }

        //Skapar user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> CreateUser(CreateUserModel newUserInput)
        {
            var newUser = await _userRepository.CreateUser(newUserInput);

            return Ok(newUser);
        }

        //Updaterar user baserat på id
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>>  UpdateUser([FromBody] UpdateUserModel updateUserInput)
        {
            var user = await _userRepository.UpdateUser(updateUserInput);
            return Ok(user);
        }
        
        //Hämtar en user baserat på Id
        [HttpGet("{userId}", Name = "GetByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserDto>> GetByUserId(string userId)
        {
			 var data = await _userRepository.GetByUserId(userId);
			 if (data.Value != null)
			 {
				 return Ok(data.Value);
			 }

			 return NotFound();
        }
    }
}
