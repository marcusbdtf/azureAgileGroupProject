using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IOrderRepository<Order> _orderRepository;

		public OrderController(IOrderRepository<Order> orderRepository)
		{
			_orderRepository = orderRepository; }

		[HttpPost]
		public async Task<IActionResult> AddOrder(OrderDto newOrder) //TODO: ska det vara ett orderobjekt eller DTO?
		{
			return Ok(await _orderRepository.AddItemAsync(ConvertTOrder(newOrder)));
		}
		
		[HttpGet] 
		public async Task<IActionResult> GetByEmail(string email) //TODO: ska det returneras orderobjekt eller DTO?
		{
			var orderToReturn = await _orderRepository.GetByEmail(email);
			
			return Ok(orderToReturn.Select(ConvertToOrderDto));
		}

		[HttpPatch]
		public async Task<IActionResult> PatchOrder(OrderDto orderToPatch)
		{
			var result = await _orderRepository.PatchOrder(ConvertTOrder(orderToPatch));

			return Ok(result);
		}


		private OrderDto ConvertToOrderDto(Order orderToConvert)
		{
			return new OrderDto()
			{
				OrderId = orderToConvert.OrderId,
				OrderProducts = orderToConvert.OrderProducts,
				UserId = orderToConvert.UserId,
				OrderDate = orderToConvert.OrderDate,
				Status = orderToConvert.Status
			};
		}

		private Order ConvertTOrder(OrderDto orderDtoToConvert)
		{
			return new Order()
			{
				OrderId = orderDtoToConvert.OrderId,
				OrderProducts = orderDtoToConvert.OrderProducts,
				UserId = orderDtoToConvert.UserId,
				OrderDate = orderDtoToConvert.OrderDate,
				Status = orderDtoToConvert.Status
			};
		}
	}

	
}
