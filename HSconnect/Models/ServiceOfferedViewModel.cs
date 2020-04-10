using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
    public class ServiceOfferedViewModel
    {
        public Provider Provider { get; set; }
        public Address Address { get; set; }
        public Demographic Demographic { get; set; }
        public Service Service { get; set; }
        public Category Category { get; set; }
        public string Cost { get; set; }
        public int IsMale { get; set; }
        public int GenderSelection { get; set; }
        public int FamilySelection { get; set; }
        public int SmokingSelection { get; set; }
        public int AgeSensitive { get; set; }
    }
}
