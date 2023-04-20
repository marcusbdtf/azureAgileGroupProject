using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
			private readonly IUserRepository _userRepository;

		public EventController(IEventRepository<Event> eventRepository, IUserRepository userRepository)
			{
				_eventRepository = eventRepository;
				_userRepository = userRepository;
			}

		[HttpGet("all")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<IActionResult> GetAllEvents()
		{
			var result = await _eventRepository.GetEvents();

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
				RegisteredCustomers = eventItem.RegisteredCustomersDtos.Select(GetUserByEmail).ToList(),
				Date = eventItem.Date
			};

			var result = await _eventRepository.AddEvent(newEvent);
			return Ok(result);
		}


		[HttpPatch]
		public async Task<IActionResult> AddCustomerToEvent(UserDto userDto, int eventId)
		{
			var result = await _eventRepository.AddUserToEvent(GetUserByEmail(userDto), eventId);
			return Ok(result);
		}

		private EventDto ConvertToEventDto(Event eventToConvert)
		{
			return new EventDto()
			{
				Id = eventToConvert.Id,
				Name = eventToConvert.Name,
				Description = eventToConvert.Description,
				RegisteredCustomersDtos = eventToConvert.RegisteredCustomers.Select(ConvertUserToDto).ToList(),
				Date = eventToConvert.Date
			};
		}

		private UserDto ConvertUserToDto(User userToConvert)
		{
			return new UserDto()
			{
				Email = userToConvert.Email,
				FirstName = userToConvert.FirstName,
				LastName = userToConvert.LastName,
				Address = new AddressDto()
				{
					AddressId = userToConvert.Address.AddressId,
					Street = userToConvert.Address.Street,
					City = userToConvert.Address.City,
					PostNumber = userToConvert.Address.PostNumber
				}
			};
		}


		private User GetUserByEmail(UserDto userToFind)
		{
			var user = _userRepository.GetUserByEmail(userToFind.Email);

			return user;
		}

	}
}
