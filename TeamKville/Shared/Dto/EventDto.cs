using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamKville.Shared.Dto
	{
	public class EventDto
		{
			public int Id { get; set; }
		[Required(ErrorMessage = "Eventet måste ha ett namn!")]
			public string Name { get; set; }
			[Required(ErrorMessage = "Eventet måste ha en beskrivning!")]
			public string Description { get; set; }
			public List<UserDto> RegisteredCustomersDtos { get; set; } = new List<UserDto>();
			public DateTime Date { get; set; }
		}
	}
