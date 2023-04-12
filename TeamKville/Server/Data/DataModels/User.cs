using System.ComponentModel.DataAnnotations;

namespace TeamKville.Server.Data.DataModels
{
	public class User
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public int AddressId { get; set; }
		public Address Address { get; set; }

	}
}
