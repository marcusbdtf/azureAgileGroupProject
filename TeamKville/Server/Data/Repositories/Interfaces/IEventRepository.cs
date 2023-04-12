using TeamKville.Server.Data.DataModels;

namespace TeamKville.Server.Data.Repositories.Interfaces;

public interface IEventRepository <T>
{
	Task<IEnumerable<T>> GetEvents(); //Func<IEnumerable<T>, bool> func);
	Task<string> AddEvent(T item);
	//Task<string> AddUserToEvent(User user, int id);
}