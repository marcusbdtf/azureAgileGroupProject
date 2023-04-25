using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Communication.Email;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public EmailController(IUserRepository userRepository)
        {
	        _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<string> SendEmail(EmailDto emailDto)
        {

	        if (_userRepository.GetUserByEmail(emailDto.Sender) == null)
		        return $"Email not sent.";

	        return "Email sent";

	        //Flytta detta till externfil o gör en service av det att injecta?:
	        //var key = new AzureKeyCredential("key From Azure vault?");
	        //var endpoint = new Uri("The endpoint");

	        //var emailClient = new EmailClient(endpoint, key);


	        //var subject = $"Proposal of new Event:{emailToSend.Header}";
	        //var htmlContent = $"<html><body><h1>Proposal of new event</h1></br>{emailToSend.Body}</body></html>";
	        //var sender = emailToSend.Sender;
	        //var recipient = "amdin@kville.se";

	        //try
	        //{
	        //    var emailSendOperation = await emailClient.SendAsync(
	        //    Azure.WaitUntil.Completed,
	        //    sender,
	        //    recipient,
	        //    subject,
	        //    htmlContent);

	        //    var status = emailSendOperation.Value.Status;
	        //    var operationId = emailSendOperation.Id;

	        //    return status.ToString();
	        //}
	        //catch (RequestFailedException ex)
	        //{
	        //    return $"Email send operation failed. {ex.ErrorCode} {ex.Status}";
	        //}
        }


        //[HttpGet]
        //public async Task<IEnumerable<EmailDto>> GetAllEmails()
        //{

        //}
    }
}
