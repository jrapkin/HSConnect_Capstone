using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
	public class Demographic
	{
		[Key]
		public int Id { get; set; }
		public bool? FamilyFriendly { get; set; }
		public int? LowIncomeThreshold { get; set; }
		public bool? IsAgeSensitive { get; set; }
		public bool? IsMale {get; set;}
		public bool? SmokingIsAllowed { get; set; }
	}
}
