using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
    public class ServiceOfferedViewModel
    {
        public List<ServiceOffered> ServicesOffered { get; set; }
        public Provider Provider { get; set; }
        public Address Address { get; set; }
        public Demographic Demographic { get; set; }
        public Service Service { get; set; }
        public Category Category { get; set; }
        public List<Provider> Providers { get; set; }
        public List<Category> Categories { get; set; }
        public List<Service> Services { get; set; }
        public Dictionary<int, string> GenderOptions { get; set; }
        public Dictionary<int, string> SmokingOptions { get; set; }
        public Dictionary<int, string> FamilyFriendlyOptions { get; set; }
        [Display(Name="Provider")]
        public int ProviderId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Service")]
        public int ServiceId { get; set; }
        public string Cost { get; set; }
        public int IsMale { get; set; }
        [Display(Name = "Gender Specific?")]
        public int GenderSelection { get; set; }
        [Display(Name = "Family Friendly?")]
        public int FamilySelection { get; set; }
        [Display(Name = "Smoking Allowed?")]
        public int SmokingSelection { get; set; }
        [Display(Name = "Age Sensitive?")]
        public int AgeSensitive { get; set; }
        [Display(Name = "Resource")]
        public int ServiceOfferedId { get; set; }
        [Display(Name = "Minimum Cost")]
        public string MinCost { get; set; }
        [Display(Name = "Maximum Cost")]
        public string MaxCost { get; set; }
    }
}
