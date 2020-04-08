using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Contracts;
using HSconnect.Models;
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
    }
}
