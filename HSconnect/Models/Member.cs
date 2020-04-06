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
		public string PhoneNumber { get; set; }
		[Required]
		public string EmailAddress { get; set; }
		[Required]
		public int Income { get; set; }
		public bool IsActiveMember { get; set; }
		[ForeignKey("Address")]
		public int? AddressId { get; set; }
		public Address Address { get; set; }
		[ForeignKey("Chart")]
		public int? ChartId { get; set; }
		public Chart Chart { get; set; }
		[ForeignKey("Demographic")]
		public int? DemographicId { get; set; }
		public Demographic Demographic { get; set; }
		[ForeignKey("ManagedCareOrganization")]
		public int? ManagedCareOrganizationId { get; set; }
		public ManagedCareOrganization ManagedCareOrganization { get; set; }
	}
}
