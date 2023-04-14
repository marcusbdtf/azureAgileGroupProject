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
        public async Task<IActionResult> GetAllUsers()
        {
            var data = await _userRepository.GetAllUsers();
            

            return Ok(data);
        }

        //Skapar user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateUser(CreateUserModel newUserInput)
        {
            var newUser = await _userRepository.CreateUser(newUserInput);

            return Ok(newUser);
        }

        //Updaterar user baserat på id
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult>  UpdateUser([FromBody] UpdateUserModel updateUserInput)
        {
            var user = await _userRepository.UpdateUser(updateUserInput);
            return Ok(user);
        }
        
        //Hämtar en user baserat på Id
        [HttpGet("{userId}", Name = "GetByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByUserId(string userId)
        {
			 var data = await _userRepository.GetByUserId(userId);

			 return Ok(data);

        }

        //Hämtar en shoppingCart baserat på UserId
        [HttpGet("shoppingcart/{userId}", Name = "GetShoppingCartByUserId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetShoppingCartByUserId(string userId)
        {
	        var data = await _userRepository.GetShoppingCartByUserId(userId);
		    
	        return Ok(data);
        }

		//Lägga till produkt i shoppingcart
		[HttpPost("shoppingcart/add", Name = "AddProductToShoppingCart")]
		[ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddProductToShoppingCart([FromBody] AddProductToShoppingCartModel input)
        {
	        var addedProductToShoppingCart = await _userRepository.AddProductToShoppingCart(input);

	        return Ok(addedProductToShoppingCart);
        }


        //TODO
        //Delete from ShoppingCart
        //Empty shoppingCart
        //decrease/increase implementera??
	}
}
