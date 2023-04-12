namespace TeamKville.Server.Data.DataModels
	{
	public class Event
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			//public List<User> RegisteredCustomers { get; set; } = new List<User>();
			public DateTime Date { get; set; }
		}
	}
