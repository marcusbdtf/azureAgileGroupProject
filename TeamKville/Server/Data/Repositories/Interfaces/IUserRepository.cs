using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<IEnumerable<UserDto>> GetAllUsers();
		Task<string> CreateUser(CreateUserModel newUserInput);
		Task<string> UpdateUser(UpdateUserModel updateUserInput);
		Task<UserDto> GetByUserId(string userId);
		Task<ShoppingCartDto> GetShoppingCartByUserId(string userId);
		Task<string> AddProductToShoppingCart(AddProductToShoppingCartModel input);
		Task<string> EmptyShoppingCart(string userId);
		Task<string> IncreaseShoppingCartProduct(string userId, int productId);
		Task<string> DecreaseShoppingCartProduct(string userId, int productId);

		Task<string> DeleteCartItemFromShoppingCart(string userId, int productId);
		User? GetUserByEmail(string email);
	}
}
