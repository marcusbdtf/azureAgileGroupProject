using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Dto
{
	public class UserDto
	{
		public string UserId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public bool IsAdmin { get; set; }

		public AddressDto Address { get; set; }

		public ShoppingCartDto? ShoppingCart { get; set; }
	} 
}
