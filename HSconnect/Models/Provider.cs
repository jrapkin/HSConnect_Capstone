using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HSconnect.Models
{
	public class Provider
	{
		[Key]
		public int Id {get; set;}
		public string ProviderName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		//partnership collection?
		[ForeignKey("IdentityUser")]
		public string IdentityUserId { get; set; }
		public IdentityUser IdentityUser { get; set; }
		[NotMapped]
		public List<Partnership> Partnerships { get; set; }
		[NotMapped]
		public ICollection<Chart> Charts { get; set; }
	}
}
