using Microsoft.AspNetCore.Mvc;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories.Interfaces
{
	public interface IUserRepository
	{
		Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers();
		Task<string> CreateUser(CreateUserModel newUserInput);
		Task<string> UpdateUser(UpdateUserModel updateUserInput);
		Task<ActionResult<UserDto>> GetByUserId(string userId);
	}
}
