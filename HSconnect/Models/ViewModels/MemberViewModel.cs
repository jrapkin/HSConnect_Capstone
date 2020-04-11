using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Models;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace HSconnect.Models
{
	public class MemberViewModel
	{
		public Member Member { get; set; }
		public SocialWorker SocialWorker { get; set; }
		public Address Address { get; set; }
		public SelectList ManagedCareOrganizations { get; set; }
		public SelectList Gender { get; set; }
		public int ManagedCareOrganizationId { get; set; }
		public int GenderSelection { get; set; }
	}
}
