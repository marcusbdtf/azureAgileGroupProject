namespace TeamKville.Server.Data.Repositories.Interfaces;

public interface IMessageInterface<T>
{
	Task<string> AddItem(T item);
	Task<string> DeleteItem(int id);
	Task<IEnumerable<T>> GetItems();
	Task<T?> UpdateItem(int id);
}