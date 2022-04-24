using System;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace API.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IMassegeRepozitory _massegeRepozitory;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public MessageHub(IMassegeRepozitory massegeRepozitory,
         IMapper mapper,
         IUserRepository userRepository)
        {
            this._massegeRepozitory = massegeRepozitory;
            this._mapper = mapper;
            _userRepository = userRepository;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["username"].ToString();
            var GroupName = GetGroupeName(Context.User.GetUsername(), otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);

            var messages = await _massegeRepozitory.GetMassegeThread(Context.User.GetUsername(), otherUser);
            await Clients.Group(GroupName).SendAsync("RecivedMessageTread", messages);

        }
     
     public override async Task OnDisconnectedAsync(Exception exception)
     {

          await base.OnDisconnectedAsync(exception);
     }

    
        public async Task SandMessage(CreateMessageDto createMessage)
        {
            var username = Context.User.GetUsername();
            if(createMessage.RecipientUserName==username)
            {throw new HubException("You can't send message to yourself.");}

            var sender = await _userRepository.GetUserByUserNameAsync(username);
            var Recipient = await _userRepository.GetUserByUserNameAsync(createMessage.RecipientUserName);

            if (Recipient==null)
            {throw new HubException("User not found");}

            var message = new Message {
                Sender = sender,
                Recipient = Recipient,
                SenderUserName = sender.UserName,
                RecipientUserName = Recipient.UserName,
                Content = createMessage.Content
            };

              _massegeRepozitory.AddMessage(message);

              if (await _massegeRepozitory.SaveAllAsync())
              {
                var group = GetGroupeName(sender.UserName, Recipient.UserName);
                await Clients.Groups(group).SendAsync("NewMessages", message);
              } 
        }

     private string GetGroupeName(string current, string other)
     {
         var stringCompare = string.CompareOrdinal(current,other) < 0;
         return stringCompare ? $"{current}-{other}" : $"{other}-{current}";
     }
    }
}