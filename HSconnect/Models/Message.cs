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
        [Display(Name = "From")]
        public string UserFromID { get; set; }
        [Display(Name = "To")]
        public string UserToId { get; set; }
        [Display(Name = "Message")]
        public string MessageContent { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
