using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;

namespace YourNamespace
{
    public class StripeOrderService
    {
        private readonly IConfiguration _configuration;

        public StripeOrderService(IConfiguration configuration)
        {
            _configuration = configuration;
            StripeConfiguration.ApiKey = _configuration.GetSection("Stripe")["SecretKey"];
        }

        public async Task<OrderDetails> RetrieveOrderDetails(string sessionId)
        {
            var service = new SessionService();
            var options = new SessionGetOptions
            {
                Expand = new List<string>
                {
                    "line_items"
                }
            };

            var session = await service.GetAsync(sessionId, options);
            var lineItems = session.LineItems.Data;

            var orderDetails = new OrderDetails
            {
                SessionId = sessionId,
                LineItems = new List<OrderLineItem>()
            };

            foreach (var lineItem in lineItems)
            {
                orderDetails.LineItems.Add(new OrderLineItem
                {
                    ProductId = lineItem.Price.ProductId,
                    Quantity = (int)lineItem.Quantity,
                    UnitPrice = lineItem.Price.UnitAmount
                });
            }

            return orderDetails;
        }
    }

    public class OrderDetails
    {
        public string SessionId { get; set; }
        public List<OrderLineItem> LineItems { get; set; }
    }

    public class OrderLineItem
    {
        public string ProductId { get; set; }
        public long? UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}