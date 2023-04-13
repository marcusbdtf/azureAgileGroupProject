using Microsoft.AspNetCore.Mvc;
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
	}
}
