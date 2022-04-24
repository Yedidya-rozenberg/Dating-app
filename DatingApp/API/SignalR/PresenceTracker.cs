using System.Linq;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace API.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<string>> OnLineUsers = new();
        
        // private static readonly ConcurrentDictionary<string, List<string>> OnLineUsers = new();

        public Task UserConnected(string username, string connectionId)
        {
            lock(OnLineUsers)
            {
                if(!OnLineUsers.ContainsKey(username))
                {
                    OnLineUsers.Add(username, new List<string>());
                }
                OnLineUsers[username].Add(connectionId);
            }
            return Task.CompletedTask;

        }

          public Task UserDisconnected(string username, string connectionId)
        {
         if(!OnLineUsers.ContainsKey(username)) return Task.CompletedTask;

            lock(OnLineUsers)
            {
                 OnLineUsers[username].Remove(connectionId);
                 if(OnLineUsers[username].Count==0)
                {
                    OnLineUsers.Remove(username);
                }
            }
            
            return Task.CompletedTask;
        }
    
    public Task<string[]> GetOnlineUsers()
    {
        string[] onlineUsers;
        lock (OnLineUsers)
        {
           onlineUsers = OnLineUsers.OrderBy(x=>x.Key).Select(x=>x.Key).ToArray(); 
        }
        return Task.FromResult(onlineUsers);
    }
    }
}
