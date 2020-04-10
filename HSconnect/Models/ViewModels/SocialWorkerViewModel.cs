using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Models;

namespace HSconnect.Models
{
	public class SocialWorkerViewModel
	{
		SocialWorker SocialWorker { get; set; }
		ServiceOffered ServiceOffered { get; set; }
		List<Demographic> Demographics { get; set; }
	}
}
