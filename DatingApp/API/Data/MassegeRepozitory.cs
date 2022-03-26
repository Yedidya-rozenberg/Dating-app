using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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

                public Task<IEnumerable<MessageDto>> GetMassegeThread(int currentUserId, int recipientId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _Context.SaveChangesAsync()>0);
        }
    }
}