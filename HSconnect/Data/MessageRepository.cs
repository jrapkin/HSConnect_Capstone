using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HSconnect.Data
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {

        }
        public ICollection<Message> GetMessagesByUser(string userFromId, string userToId)
        {
            return FindByCondition(m => m.UserFromID == userFromId && m.UserToId == userToId).ToList();
        }
        public void CreateMessage(string userFromId, string userToId, string messageContent)
        {
            Message message = new Message()
            {
                UserFromID = userFromId,
                UserToId = userToId,
                MessageContent = messageContent,
                TimeStamp = DateTime.Now
            };
            Create(message);
        }
    }
}
