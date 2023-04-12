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
		private readonly IProductRepository _productRepository;

		public OrderController(IOrderRepository<Order> orderRepository, IProductRepository productRepository)
		{
			_orderRepository = orderRepository;
			_productRepository = productRepository;
		}

		[HttpPost]
		public async Task<IActionResult> AddOrder(OrderDto newOrder) //TODO: ska det vara ett orderobjekt eller DTO?
		{
			return Ok(await _orderRepository.AddItemAsync(await ConvertToOrder(newOrder)));
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
			var result = await _orderRepository.PatchOrder(await ConvertToOrder(orderToPatch));

			return Ok(result);
		}


		private OrderDto ConvertToOrderDto(Order orderToConvert)
		{
			return new OrderDto()
			{
				OrderId = orderToConvert.OrderId,
				OrderedProductsDto = orderToConvert.OrderedProducts.Select(pq =>
				
					new ProductQuantityDto()
					{
						ProductDto = new ProductDto()
						{
							Age = pq.Product.Age,
							Category = new CategoryDto()
							{
								Name = pq.Product.Category.Name,
								CategoryId = pq.Product.CategoryId
							},
							Description = pq.Product.Description,
							Comments = pq.Product.Comments.Select(ConvertCommentsToDto),
							IsActive = pq.Product.IsActive,
							Name = pq.Product.Name,
							Price = pq.Product.Price
						},
						Quantity = pq.Quantity,
					}
				).ToList(),
				UserId = orderToConvert.UserId,
				OrderDate = orderToConvert.OrderDate,
				Status = orderToConvert.Status,
				Name = orderToConvert.Name,
				City = orderToConvert.City,
				PostalCode = orderToConvert.PostalCode,
				Street = orderToConvert.Street
			};
		}

		private CommentDto ConvertCommentsToDto(Comment arg)
		{
			return new CommentDto()
			{
				CommentId = arg.CommentId,
				Date = arg.Date,
				Name = arg.Name,
				Text = arg.Text,
				Rating = arg.Rating
			};
		}

		private async Task<Order> ConvertToOrder(OrderDto orderDtoToConvert)
		{
			var newOrder = new Order()
			{
				OrderId = orderDtoToConvert.OrderId,
				OrderedProducts = orderDtoToConvert.OrderedProductsDto.Select(pqDto => 
					new ProductQuantity()
						{
							Product = _productRepository.GetProductById(pqDto.ProductDto.Id),
							Quantity = pqDto.Quantity
						}).ToList(),
				UserId = orderDtoToConvert.UserId,
				OrderDate = orderDtoToConvert.OrderDate,
				Status = orderDtoToConvert.Status,
				Name = orderDtoToConvert.Name,
				City = orderDtoToConvert.City,
				PostalCode = orderDtoToConvert.PostalCode,
				Street = orderDtoToConvert.Street
			};

			return newOrder;
		}
	}
}
