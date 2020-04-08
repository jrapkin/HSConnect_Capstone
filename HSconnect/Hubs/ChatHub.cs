using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IRepositoryWrapper _repo;
        public ChatHub(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        public ICollection<string> GetArchivedMessages(string userFromId, string userToId)
        {
            return _repo.Message.GetMessagesByUser(userFromId, userToId).Select(m => m.UserFromID + " says " + m.UserToId).ToList();
        }
        public void ArchiveMessage(string userFromId, string userToId, string messageContent)
        {
            _repo.Message.CreateMessage(userFromId, userToId, messageContent);
            _repo.Save();
        }
    }
}
