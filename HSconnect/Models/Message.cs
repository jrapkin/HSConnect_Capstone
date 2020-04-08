using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string UserFromID { get; set; }
        public string UserToId { get; set; }
        public string MessageContent { get; set; }
    }
}
