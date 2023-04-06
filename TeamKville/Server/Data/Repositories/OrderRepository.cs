using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Server.Data.DataModels;

namespace TeamKville.Server.Data.Repositories;

public class OrderRepository : IOrderRepository<Order>
{
	private readonly DataContext _dataContext;

	public OrderRepository(DataContext dataContext)
	{
		_dataContext = dataContext;
	}

	public async Task<string> AddItemAsync(Order item)
	{
		//TODO: Checkar här ovanför om ordern redan finns eller liknande.
		
		await _dataContext.Orders.AddAsync(item);

		await _dataContext.SaveChangesAsync();

		return "Saved to database";
	}
	
	/*
	public async Task<IEnumerable<Order>> GetByEmail(string email)
	{
		//Metod för att hitta användaren baserat på email. Och sen ta 
		//var userToFindID = _dataContext.Users.FirstOrDefault(x => x.Email == email);

		//if (userToFindID != null)
		//{
		//	var orders = _dataContext.Orders.Where(o => o.UserId.Equals(userToFindID.Id));
		//	return orders;
		//}

		return null;
	}*/

	public async Task<string> PatchOrder(Order item)
	{
		var orderToUpdate = await _dataContext.Orders.FirstOrDefaultAsync(o => o.OrderId == item.OrderId);

		if (orderToUpdate != null)
		{
			//annat som ska uppdateras?
			orderToUpdate.OrderDate = item.OrderDate;
			orderToUpdate.Status = item.Status;

			var stateUpdates = await _dataContext.SaveChangesAsync();

			return $"Updated order, updates: {stateUpdates}";
		}

		return "Order not found.";
	}
}