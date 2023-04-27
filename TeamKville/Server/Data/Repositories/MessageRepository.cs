using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;

namespace TeamKville.Server.Data.Repositories;

public class MessageRepository : IMessageInterface<Message>
{
	private readonly DataContext _dataContext;
	public MessageRepository(DataContext dataContext)
	{
		_dataContext = dataContext;
	}

	public async Task<string> AddItem(Message item)
	{
		var result = await _dataContext.Messages.AddAsync(item);

		await _dataContext.SaveChangesAsync();

		return $"Message: {result.Entity.Header} sent";
	}

	public async Task<string> DeleteItem(int id)
	{
		var toDelete = await _dataContext.Messages.FirstOrDefaultAsync(m => m.Id.Equals(id));

		if (toDelete == null)
		{
			return $"Message with id:{id} not found";
		}

		_dataContext.Messages.Remove(toDelete);

		var result = await _dataContext.SaveChangesAsync();

		return $"{result} message deleted";
	}

	public async Task<IEnumerable<Message>> GetItems()
	{
		return _dataContext.Messages;
	}


	public async Task<Message?> UpdateItem(int id)
	{
		var itemToUpdate = await _dataContext.Messages.FirstOrDefaultAsync(m => m.Id.Equals(id));

		if (itemToUpdate == null)
		{
			return null;
		}

		itemToUpdate.IsRead = true;

		var result = await _dataContext.SaveChangesAsync();

		if (result > 0)
		{
			return itemToUpdate;
		}

		return null;
	}
}