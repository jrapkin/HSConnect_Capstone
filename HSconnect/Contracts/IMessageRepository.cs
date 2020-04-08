using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Models;

namespace HSconnect.Contracts
{
    public interface IMessageRepository : IRepositoryBase<Message>
    {
        ICollection<Message> GetMessagesByUser(string userFromId, string userToId);
        void CreateMessage(string userFromId, string userToId, string messageContent);
    }
}
