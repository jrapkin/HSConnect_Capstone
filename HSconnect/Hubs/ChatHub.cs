using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.AspNetCore.SignalR;
using System;
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
        public async Task SendMessage(string userFrom, string userTo, string message, string timeStamp)
        {
            await Clients.All.SendAsync("ReceiveMessage", userFrom, userTo, message, timeStamp);
        }
        public ICollection<string> GetArchivedMessages(string userFromId, string userToId)
        {
            List<string> messages = _repo.Message.GetMessagesByUser(userFromId, userToId).Select(m => m.TimeStamp.ToShortDateString() + " " + m.UserFromID + " says " + m.MessageContent).ToList();
            return messages;
        }
        public void ArchiveMessage(string userFromId, string userToId, string messageContent)
        {
            _repo.Message.CreateMessage(userFromId, userToId, messageContent);
            _repo.Save();
        }
    }
}
