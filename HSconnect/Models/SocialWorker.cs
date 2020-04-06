using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace HSconnect.Models
{
	public class SocialWorker
	{
		public int SocialWorkerId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Company { get; set; }
		[ForeignKey("IdentityUser")]
		public int IdentityUserId { get; set; }
		public IdentityUser IdentityUser { get; set; }

	}
}
