using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using YourNamespace;

[Route("webhook")]
[ApiController]
public class StripeWebhookController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly StripeOrderService _stripeOrderService;
    private readonly IOrderRepository<Order> _orderRepository;
    private readonly IProductRepository _productRepository;

    public StripeWebhookController(IConfiguration configuration, IOrderRepository<Order> orderRepository, IProductRepository productRepository)
    {
        _configuration = configuration;
        _stripeOrderService = new StripeOrderService(configuration);
        _orderRepository = orderRepository;
        _productRepository = productRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var webhookSecret = _configuration.GetSection("Stripe")["WebhookSecret"];
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], webhookSecret);

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                var sessionId = session.Id;

                var orderDetails = await _stripeOrderService.RetrieveOrderDetails(sessionId);

                var orderDto = new OrderDto
                {
                    UserId = TeamKville.Shared.SharedClass.activeUser.UserId, 
                    OrderDate = DateTime.UtcNow,
                    Status = true, 
                    OrderedProductsDto = orderDetails.LineItems.Select(item => new ProductQuantityDto
                    {
                        ProductDto = new ProductDto { Id = int.Parse(item.ProductId) },
                        Quantity = item.Quantity,
                    }).ToList()
                };

                var orderToSave = new Order
                {
                    UserId = orderDto.UserId,
                    OrderDate = orderDto.OrderDate,
                    Status = orderDto.Status,
                    Name = orderDto.Name,
                    City = orderDto.City,
                    PostalCode = orderDto.PostalCode,
                    Street = orderDto.Street,
                    OrderedProducts = orderDto.OrderedProductsDto.Select(pqDto =>
                        new ProductQuantity
                        {
                            Product = _productRepository.GetProductById(pqDto.ProductDto.Id),
                            Quantity = pqDto.Quantity
                        }).ToList()
                };

                await _orderRepository.AddItemAsync(orderToSave);
            }
            else
            {
                Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest();
        }
    }
}
