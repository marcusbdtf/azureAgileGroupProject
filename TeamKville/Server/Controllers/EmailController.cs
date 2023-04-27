using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Communication.Email;
using TeamKville.Server.Data.DataModels;
using TeamKville.Server.Data.Repositories.Interfaces;
using TeamKville.Shared.Dto;

namespace TeamKville.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
		private readonly IMessageInterface<Message> _messageInterface;

        public EmailController(IUserRepository userRepository, IMessageInterface<Message> messageInterface)
        {
	        _userRepository = userRepository;
			_messageInterface = messageInterface;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(EmailDto emailDto)
        {

	        if (_userRepository.GetUserByEmail(emailDto.Email) == null)
		        return BadRequest("Message Not sent");

	        var result = await _messageInterface.AddItem(ConvertToMessage(emailDto));

	        return Ok(result);

        }


		[HttpGet]
		public async Task<IActionResult> GetAllEmails()
		{
			var result = await _messageInterface.GetItems();
			
			return Ok(result.Select(ConvertMessageToDto));

		}

		[HttpPatch]
		public async Task<IActionResult> UpdateIsRead(EmailDto update)
		{
			var result = await _messageInterface.UpdateItem(update.Id);

			if (result == null)
				return BadRequest($"Message with id {update.Id} not found.");

			return Ok(result);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteMessage(int id)
		{
			var result = await _messageInterface.DeleteItem(id);

			return Ok(result);
		}

		private Message ConvertToMessage(EmailDto emailDto)
		{
			return new Message()
			{
				Id = emailDto.Id,
				Body = emailDto.Body,
				SenderName = emailDto.SenderName,
				Header = emailDto.Header,
				Email = emailDto.Email,
				IsRead = emailDto.IsRead,
				TimeSent = emailDto.TimeSent
			};
		}

		private EmailDto ConvertMessageToDto(Message message)
		{
			return new EmailDto()
			{
				Id = message.Id,
				Body = message.Body,
				SenderName = message.SenderName,
				Header = message.Header,
				Email = message.Email,
				IsRead = message.IsRead,
				TimeSent = message.TimeSent
			};
		}
    }
}
