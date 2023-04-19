using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Dto
	{
	public class EventDto
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public List<UserDto> RegisteredCustomersDtos { get; set; } = new List<UserDto>();
			public DateTime Date { get; set; }
		}
	}
