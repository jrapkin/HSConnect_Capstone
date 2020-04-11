using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
	public class Member
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		[Required]
		[Display(Name = "Email Address")]
		public string EmailAddress { get; set; }
		[Required]
		public int Income { get; set; }
		[Required]
		[Display(Name = "Gender")]
		public bool? IsMale { get; set; }
		[Required]
		public int Age { get; set; }
		[Required]
		[Display (Name ="Activity Status")]
		public bool IsActiveMember { get; set; }
		[ForeignKey("Address")]
		public int? AddressId { get; set; }
		public Address Address { get; set; }
		[ForeignKey("Chart")]
		public int? ChartId { get; set; }
		public Chart Chart { get; set; }
		[ForeignKey("ManagedCareOrganization")]
		public int? ManagedCareOrganizationId { get; set; }
		public ManagedCareOrganization ManagedCareOrganization { get; set; }
	}
}
