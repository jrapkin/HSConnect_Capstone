using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
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
        public ICollection<Message> GetArchivedMessages(string userFromId, string userToId)
        {
            return _repo.Message.GetMessagesByUser(userFromId, userToId);
        }
    }
}
