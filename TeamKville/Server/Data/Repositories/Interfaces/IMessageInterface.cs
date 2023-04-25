namespace TeamKville.Server.Data.Repositories.Interfaces;

public interface IMessageInterface<T>
{
	Task<string> AddItem(T item);
	Task<string> DeleteItem(T item);
	Task<IEnumerable<T>> GetItems(T item);
}