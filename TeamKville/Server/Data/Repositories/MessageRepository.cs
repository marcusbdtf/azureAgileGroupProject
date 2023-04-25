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

	public async Task<string> DeleteItem(Message item)
	{
		var result = _dataContext.Messages.Remove(item);

		await _dataContext.SaveChangesAsync();

		return $"Message: {result.Entity.Header} sent";
	}

	public async Task<IEnumerable<Message>> GetItems(Message item)
	{
		return _dataContext.Messages;
	}
}