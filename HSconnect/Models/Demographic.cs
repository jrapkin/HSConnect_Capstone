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
		[Display(Name="Demographic")]
		public int Id { get; set; }
		[Display(Name="Family Friendly?")]
		public bool? FamilyFriendly { get; set; }
		[Display(Name="Low Income Threshold")]
		public int? LowIncomeThreshold { get; set; }
		[Display(Name="Age Specific?")]
		public bool? IsAgeSensitive { get; set; }
		[Display(Name = "Gender Specific?")]
		public bool? IsMale {get; set;}
		[Display(Name="Smoking Allowed?")]
		public bool? SmokingIsAllowed { get; set; }
	}
}
