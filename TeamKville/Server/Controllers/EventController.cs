using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Controllers
	{
		[Route("event")]
		[ApiController]
	public class EventController : ControllerBase
		{
			private readonly IEventRepository<Event> _eventRepository;

		public EventController(IEventRepository<Event> eventRepository)
			{
				_eventRepository = eventRepository;
			}

		[HttpGet("all")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetAllEvents()
		{
var result = await _eventRepository.GetEvents();

var test = result.Select(ConvertToEventDto);

			return Ok(result.Select(ConvertToEventDto));
		}

		[HttpPost("create")]
		public async Task<IActionResult> AddEvent(EventDto eventItem)
		{
			var newEvent = new Event()
			{
				Id = eventItem.Id,
				Name = eventItem.Name,
				Description = eventItem.Description,
				//RegisteredCustomers = eventItem.RegisteredCustomers, //Convertera till User
				Date = eventItem.Date

			};

var result = await _eventRepository.AddEvent(newEvent);
return Ok(result);
			
		}

//		[HttpPatch]
//		//Ta reda på vad från eventet som skickas från frontend
//		public async Task<IActionResult> AddCustomerToEvent(UserDto userDto, int eventId)
//		{
//var result = await _eventRepository.AddUserToEvent(userDto, eventId);
//return Ok(result);
//		}

		private EventDto ConvertToEventDto(Event eventToConvert)
		{
			return new EventDto()
			{
				Id = eventToConvert.Id,
				Name = eventToConvert.Name,
				Description = eventToConvert.Description,
				//RegisteredCustomersDtos = eventToConvert.RegisteredCustomers.Select(ConvertUserToDto),
Date = eventToConvert.Date
			};

		//private UserDto ConvertUserToDto(User userToConvert)

		//{
		//	return new UserDto()
		//	{
		//		Email = userToConvert.Email,
		//		FirstName = userToConvert.FirstName,
		//		LastName = userToConvert.LastName,
		//		Street = userToConvert.Street,
		//		City = userToConvert.City,
		//		PostNr = userToConvert.PostNr
		//	};



		//}
		}
		}
	}
