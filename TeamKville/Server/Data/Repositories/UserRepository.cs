using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using TeamKville.Shared.Models;
using Xamarin.Forms.Internals;

namespace TeamKville.Server.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _dbContext;

		public UserRepository(DataContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task<IEnumerable<UserDto>> GetAllUsers()
		{
			var users = await _dbContext.Users
			.Include(x => x.Address)
			.Include(x => x.ShoppingCart)
			.ThenInclude(x => x.CartItems)
			.ThenInclude(x => x.Product)
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

				},

				ShoppingCart = new ShoppingCartDto
				{
					ShoppingCartTotalPrice = x.ShoppingCart.CartItems.Select(x => x.Quantity * x.Product.Price).Sum(),
					Products = x.ShoppingCart.CartItems.Select(x => new CartItemDto
					{
						ProductName = x.Product.Name,
						Price = x.Product.Price,
						TotalProductPrice = x.Quantity * x.Product.Price,
						Quantity = x.Quantity
					}),
				}

			}).ToListAsync();

			if (!users.Any())
				return new List<UserDto>();

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
				},
				ShoppingCart = new ShoppingCart()

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

		public async Task<UserDto> GetByUserId(string userId)
		{
			var user = await _dbContext.Users
				.Include(x => x.Address)
				.Include(x => x.ShoppingCart)
				.ThenInclude(x => x.CartItems)
				.ThenInclude(x => x.Product)
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
					},
					ShoppingCart = new ShoppingCartDto
					{
						ShoppingCartTotalPrice = x.ShoppingCart.CartItems.Select(x => x.Quantity * x.Product.Price).Sum(),
						Products = x.ShoppingCart.CartItems.Select(x => new CartItemDto
						{
							ProductName = x.Product.Name,
							Price = x.Product.Price,
							TotalProductPrice = x.Quantity * x.Product.Price,
							Quantity = x.Quantity
						}),
					}
				})
				.FirstOrDefaultAsync();

			if (user == null)
				return new UserDto();

			return user;
		}

		public async Task<ShoppingCartDto> GetShoppingCartByUserId(string userId)
		{
			var shoppingCart = await _dbContext.ShoppingCarts
				.Where(x => x.UserId == userId)
				.Select(x => new ShoppingCartDto
				{
					ShoppingCartTotalPrice = x.CartItems.Select(x => x.Quantity * x.Product.Price).Sum(),
					Products = x.CartItems.Select(x => new CartItemDto
					{
						ProductName = x.Product.Name,
						Price = x.Product.Price,
						TotalProductPrice = x.Quantity * x.Product.Price,
						Quantity = x.Quantity
					}),
				}).FirstOrDefaultAsync();

			if (shoppingCart == null)
				return new ShoppingCartDto();

			return shoppingCart;
		}

		public async Task<string> AddProductToShoppingCart(AddProductToShoppingCartModel input)
		{
			var user = await _dbContext.Users
				.Include(x => x.ShoppingCart)
				.ThenInclude(x => x.CartItems)
				.ThenInclude(x => x.Product)
				.FirstOrDefaultAsync(x => x.UserId == input.UserId);

			var productFromDb = await _dbContext.Products.FirstOrDefaultAsync(x => x.ProductId == input.ProductId);

			if(user == null)
				return await Task.FromResult($"User with UserId '{input.UserId}' does not exists");

			if(productFromDb == null)
				return await Task.FromResult($"Product with ProductId '{input.ProductId} could not be found.");

			var userCartItem = user.ShoppingCart.CartItems.ToList();

			if (userCartItem.Select(x => x.ProductId).Contains(input.ProductId))
			{
				foreach (var product in userCartItem)
				{
					if (product.ProductId == input.ProductId)
					{
						product.Quantity += input.Quantity;
					}
				}
			}
			else
			{
				var newCartItem = new CartItem
				{
					ProductId = input.ProductId,
					Quantity = input.Quantity,
					ShoppingCartId = user.ShoppingCart.ShoppingCartId
				};

				user.ShoppingCart.CartItems.Add(newCartItem);
			}

			await _dbContext.SaveChangesAsync();
			return await Task.FromResult("Product added to shopping cart!");
		}
	}
}
