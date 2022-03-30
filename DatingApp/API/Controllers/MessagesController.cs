
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MessagesController : BaseApiController
    
    {
        private readonly IUserRepository _userRepository;
        private readonly IMassegeRepozitory _massegeRepozitory;
        private readonly IMapper _mapper;

        public MessagesController(IUserRepository userRepository,
                                 IMassegeRepozitory massegeRepozitory,
                                 IMapper mapper)
        {
            this._userRepository = userRepository;
            this._massegeRepozitory = massegeRepozitory;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessage)
        {
            var username = User.GetUsername();
            if(createMessage.RecipientUserName==username)
            {return BadRequest("You can't send message to yourself.");}

            var sender = await _userRepository.GetUserByUserNameAsync(username);
            var Recipient = await _userRepository.GetUserByUserNameAsync(createMessage.RecipientUserName);

            if (Recipient==null)
            {return NotFound();}

            var message = new Message {
                Sender = sender,
                Recipient = Recipient,
                SenderUserName = sender.UserName,
                RecipientUserName = Recipient.UserName,
                Content = createMessage.Content
            };

              _massegeRepozitory.AddMessage(message);

              if (await _massegeRepozitory.SaveAllAsync()) {return Ok(_mapper.Map<MessageDto>(message));}

              return BadRequest("Faild to sent message");
        }
        [HttpGet]
            public async Task<ActionResult<PageList<MessageDto>>>  GetMessagesGorUser([FromQuery]MessageParams messageParams)
            {
                messageParams.UserName = User.GetUsername();
                var messages = await _massegeRepozitory.GetMessagesForUser(messageParams);
                Response.AddPaginationHeader(messages.CurrentPage, messages.PageSize, messages.TotalCount, messages.TotalPages);
                return Ok(messages);
            }

            [HttpGet("thread/{username}")]
            public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread (string username)
            {
                var currentUsername = User.GetUsername();
                var messagesThread = await _massegeRepozitory.GetMassegeThread(currentUsername, username);
                return Ok(messagesThread);
            }
                
            
    
    }
}