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
		public ServiceOffered ServiceOffered { get; set; }
		public ManagedCareOrganization ManagedCareOrganization { get; set; }
		public List<Demographic> Demographics { get; set; }
		public SelectList ListOfOrganizations { get; set; }

	}
}
