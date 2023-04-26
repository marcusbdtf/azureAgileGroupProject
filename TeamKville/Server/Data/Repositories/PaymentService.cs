using Azure;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Data.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IConfiguration configuration, ILogger<PaymentService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public PaymentService()
        {
            StripeConfiguration.ApiKey =
                "sk_test_51N0iM9Fb1LbkT4wodWSbNxMQoTyQjMQEUHafqk54LM2JsE5ROTWILrBwZin4z2VnhnVQiFmJMWxqdWKOPKYZLeZI00hFoMXjZm";
        }
        public Session CreateCheckoutSession(List<CartItemDto> cartItems)
        {
            _logger.LogInformation("Creating checkout session...");
            var lineItems = new List<SessionLineItemOptions>();
            cartItems.ForEach(ci => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = ci.Price * 100,
                    Currency = "sek",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        //här läggar man till vad som visas i checkout page. 
                        //TODO add images t.ex: Image = "SharedClass.GetImage("product",ci.ProductName)"
                        // fungerade ej sist
                        Name = ci.ProductName,
                    }
                },
                Quantity = ci.Quantity
            }));

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:7276/success", //TODO Ändra för live
                CancelUrl = "http://localhost:7276/Cart",   //TODO Ändra för live
            };


            var service = new SessionService();
            Session session = service.Create(options);
            _logger.LogInformation("Checkout session created with URL: {Url}", session.Url);
            return session;
        }
    }
}
