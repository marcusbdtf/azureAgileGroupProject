using Microsoft.EntityFrameworkCore;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;

namespace TeamKville.Server.Data.Repositories
	{
	public class EventRepository : IEventRepository<Event>
	{
		private readonly DataContext _dataContext;
		public EventRepository(DataContext dataContext)
		{
			_dataContext = dataContext;
		}

		public async Task<IEnumerable<Event>> GetEvents() //Func<IEnumerable<Event>, bool> filter)
		{
			var result = _dataContext.Events.Include(r => r.RegisteredCustomers).ThenInclude(u => u.Address); //.FindAsync(filter);

			return result;

		}

		public async Task<string> AddEvent(Event item)
		{
			await _dataContext.Events.AddAsync(item);
			await _dataContext.SaveChangesAsync();
			return "Saved to database";

		}

		public async Task<string> AddUserToEvent(User user, int id)
		{
			var item = await _dataContext.Events.FirstOrDefaultAsync(e => e.Id == id);

			item.RegisteredCustomers.Add(user);
			await _dataContext.SaveChangesAsync();
			return "Saved to database";
		}
	}
}
