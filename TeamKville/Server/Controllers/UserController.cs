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
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserModel updateUserInput)
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

        //Tömer kundkorg baserat på UserId
        [HttpPost("shoppingcart/empty/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> EmptyShoppingCart(string userId)
        {
	        var result = await _userRepository.EmptyShoppingCart(userId);
	        return Ok(result);
        }

        //Lägger till quantity i en viss produkt baserat på id i kundkorgen
        [HttpPost("shoppingcart/increase/{userId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> IncreaseQuantity(string userId, int productId)
        {
	        var result = await _userRepository.IncreaseShoppingCartProduct(userId, productId);
	        return Ok(result);

        }

        //Tar bort quantity i en viss produkt baserat på id i kundkorgen
        [HttpPost("shoppingcart/decrease/{userId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DecreaseQuantity(string userId, int productId)
        {
	        var result = await _userRepository.DecreaseShoppingCartProduct(userId, productId);
	        return Ok(result);

        }


        [HttpPost("shoppingcart/delete-cartitem/{userId}/{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCartItemFromShoppingCart(string userId, int productId)
        {
	        var result = await _userRepository.DeleteCartItemFromShoppingCart(userId, productId);
	        return Ok(result);

        }




		//TODO
		//Delete from ShoppingCart
		//Empty shoppingCart WIP
		//decrease/increase implementera??
	}
}
