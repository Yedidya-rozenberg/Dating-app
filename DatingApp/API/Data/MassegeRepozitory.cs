using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class MassegeRepozitory : IMassegeRepozitory
    {
        private readonly DataContext _Context;
        private readonly IMapper _mapper;
        public MassegeRepozitory(DataContext dataContext, IMapper mapper)
        {
            this._mapper = mapper;
            this._Context = dataContext;
        }

        public void AddMessage(Message message)
        {
            _Context.Messages.Add(message);
        }

        public void DeleteMessage(Message message)
        {
            _Context.Messages.Remove(message);
        }

    public async Task<Message> GetMessage(int id)
        {
            return await _Context.Messages.FindAsync(id);
        }

        public async Task<PageList<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var query = _Context.Messages.AsQueryable();

           query =  messageParams.Container switch
            {
             "Inbox" =>  query.Where(m=>m.Recipient.UserName==messageParams.UserName),
             "Outbox" => query.Where(m=>m.Sender.UserName==messageParams.UserName),
             _ => query.Where(m=>m.Recipient.UserName==messageParams.UserName && m.DataRead == null)
            };

            var messages = query.ProjectTo<MessageDto>(_mapper.ConfigurationProvider);

            return await PageList<MessageDto>.CreateAsync(messages, messageParams.PageNumber,messageParams.PageSize);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _Context.SaveChangesAsync()>0);
        }

       public async Task<IEnumerable<MessageDto>> GetMassegeThread(string currentUsername, string recipientname)
        {
            var messages = await _Context.Messages
            .Include(u=>u.Sender).ThenInclude(p=>p.Photos)
            .Include(u=>u.Recipient).ThenInclude(p=>p.Photos)
            .Where(m=> 
            m.Recipient.UserName == currentUsername && m.Sender.UserName == recipientname ||
            m.Sender.UserName == currentUsername && m.Recipient.UserName == recipientname )
            .OrderBy(m=>m.MassegeSent)
            .ToListAsync();

            // var unreadMessages = messeges.Where(m=>m.DataRead == null 
            // && m.Recipient.UserName == currentUsername).ToList();
             if(await updateUnread(messages, currentUsername) == -1){
                throw new Exception("could not save to DB all the data");
            }


            return _mapper.Map<IEnumerable<MessageDto>>(messages);


        }
        private async Task<int>  updateUnread(List<Message> messages, string currentUsername)
        {
             var unreadMessages =  messages.Where(m=>m.DataRead == null 
            && m.Recipient.UserName == currentUsername).ToList();

            if(unreadMessages.Any())
            {
                foreach (var m in unreadMessages) m.DataRead = DateTime.Now;
                var rtn = await _Context.SaveChangesAsync();
                if (rtn < unreadMessages.Count)
                {
                    return -1;
                }
            }
            return 0;

        } 
    }
}