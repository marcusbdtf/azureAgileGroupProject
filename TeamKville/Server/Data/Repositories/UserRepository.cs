using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;

namespace TeamKville.Server.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _dbContext;

		public UserRepository(DataContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
		{
			var users = await _dbContext.Users
			.Include(x => x.Address)
			.Select(x => new UserDto
			{
				UserId = x.UserId,
				FirstName = x.FirstName,
				LastName = x.LastName,
				Email = x.Email,
				PhoneNumber = x.PhoneNumber,

				Address = new AddressDto
				{
					AddressId = x.Address.AddressId,
					City = x.Address.City,
					Street = x.Address.Street,
					PostNumber = x.Address.PostNumber,

				}
			}).ToListAsync();

			return users;
		}

		public Task<string> CreateUser(CreateUserModel newUserInput)
		{
			var newUser = new User
			{
				UserId = newUserInput.UserId,
				FirstName = newUserInput.FirstName,
				LastName = newUserInput.LastName,
				Email = newUserInput.Email,
				PhoneNumber = newUserInput.PhoneNumber,

				Address = new Address
				{
					City = newUserInput.Address.City,
					Street = newUserInput.Address.Street,
					PostNumber = newUserInput.Address.PostNumber,
				}
			};

			_dbContext.Users.Add(newUser);
			_dbContext.SaveChanges();

			return Task.FromResult("User successfully created");
		}

		public Task<string> UpdateUser(UpdateUserModel updateUserInput)
		{
			var userToUpdate = _dbContext.Users
				.Include(x => x.Address)
				.FirstOrDefault(x => x.UserId == updateUserInput.UserId);

			if (userToUpdate != null)
			{
				userToUpdate.FirstName = updateUserInput.FirstName;
				userToUpdate.LastName = updateUserInput.LastName;
				userToUpdate.Email = updateUserInput.Email;
				userToUpdate.PhoneNumber = updateUserInput.PhoneNumber;
				userToUpdate.Address.City = updateUserInput.Address.City;
				userToUpdate.Address.Street = updateUserInput.Address.Street;
				userToUpdate.Address.PostNumber = updateUserInput.Address.PostNumber;

				_dbContext.SaveChanges();
				return Task.FromResult("User successfully updated");
			}

			return Task.FromResult("User could not be updated");

		}

		public async Task<ActionResult<UserDto>> GetByUserId(string userId)
		{
			var user = await _dbContext.Users
				.Include(x => x.Address)
				.Where(x => x.UserId == userId)
				.Select(x => new UserDto
				{
					UserId = x.UserId,
					FirstName = x.FirstName,
					LastName = x.LastName,
					Email = x.Email,
					PhoneNumber = x.PhoneNumber,
					Address = new AddressDto
					{
						AddressId = x.Address.AddressId,
						City = x.Address.City,
						Street = x.Address.Street,
						PostNumber = x.Address.PostNumber,
					}
				})
				.FirstOrDefaultAsync();

			return user;

		}
	}
}
