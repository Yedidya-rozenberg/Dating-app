using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.helpers;

namespace API.Interfaces
{
    public interface IMassegeRepozitory
    {
        void AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        Task<PageList<MessageDto>> GetMessagesForUser(MessageParams messageParams);

        Task<IEnumerable<MessageDto>> GetMassegeThread(int currentUserId, int recipientId);

        Task<bool> SaveAllAsync();
    }
}
