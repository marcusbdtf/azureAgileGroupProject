﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;
using System;
using TeamKville.Shared;

namespace TeamKville.Server.Controllers
{

    [ApiController]
    [Route("/checkout")]

    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateCheckoutSession(List<CartItemDto> cartItems)
        {

            try
            {
                var cartItemDtos = cartItems.Select(ci => new CartItemDto
                {
                    ProductName = ci.ProductName,
                    Price = ci.Price,
                    Quantity = ci.Quantity,
                    TotalProductPrice = ci.Price * ci.Quantity
                }).ToList();

                var session = _paymentService.CreateCheckoutSession(cartItemDtos);
                Console.WriteLine($"Session: {session}");

                if (string.IsNullOrEmpty(session.Url))
                {
                    return BadRequest(new { message = "The URL from the session is empty or null." });
                }
                Response.Cookies.Append("CookieName", "CookieValue", new CookieOptions()
                {
                    Secure = true, // Only send the cookie over HTTPS
                    SameSite = SameSiteMode.None // Allow cross-site cookies
                });
                return Ok(session.Url);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating checkout session: {ex.Message}"); // Log the error message
                return BadRequest(new { message = $"Error creating checkout session: {ex.Message}" });
            }
        }

        [HttpGet("success")]
        [AllowAnonymous]
        public IActionResult Success()
        {
            // You can perform any post-payment processing here, e.g., updating the order status in your database.

            // Redirect the user to the appropriate success page in your application.
            return Redirect("/"); // Replace "/YourSuccessPage" with the actual path to your success page.
        }

    }
}