using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Models;

namespace HSconnect.Models
{
	public class SocialWorkerViewModel
	{
		public SocialWorker SocialWorker { get; set; }
		public ServiceOffered ServiceOffered { get; set; }
		public List<Member> Members { get; set; }
		public List<Chart> Charts { get; set; }

	}
}
