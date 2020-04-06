using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
	public class Partnership
	{
		[Key]
		public int Id { get; set; }
		[ForeignKey("Provider")]
		public int? ProviderId { get; set; }
		public Provider Provider { get; set; }
		[ForeignKey("ManagedCareOrganization")]
		public int? ManagedCareOrganizationId { get; set; }
		public ManagedCareOrganization ManagedCareOrganization { get; set; }
	}
}
