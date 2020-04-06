using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
	public class Chart
	{
		[Key]
		public int Id { get; set; }
		public bool ServiceIsActive { get; set; }
		public bool? ReferralAccepted { get; set; }
		public DateTime Date { get; set; }
		[ForeignKey ("SocialWorker")]
		public int? SocialWorkerId { get; set; }
		public SocialWorker SocialWorker { get; set; }
		[ForeignKey("ServiceOffered")]
		public int? ServiceOfferedId { get; set; }
		public ServiceOffered ServiceOffered { get; set; }



	}
}
