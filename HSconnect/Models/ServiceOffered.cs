using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
	public class ServiceOffered
	{
		[Key]
		public int Id { get; set; }
		public string Cost { get; set; }
		[ForeignKey("Provider")]
		public int? ProviderId { get; set; }
		public Provider Provider { get; set; }
		[ForeignKey("Category")]
		[Display(Name = "Category")]
		public int? CategoryId { get; set; }
		public Category Category { get; set; }
		[ForeignKey("Address")]
		public int? AddressId { get; set; }
		public Address Address { get; set; }
		[ForeignKey("Demographic")]
		[Display(Name = "Demographic")]
		public int? DemographicId { get; set; }
		public Demographic Demographic { get; set; }
		[ForeignKey("Service")]
		[Display(Name = "Service")]
		public int? ServiceId { get; set; }
		public Service Service { get; set; }
		[NotMapped]
		public Address AddressForCreateView { get; set; }
		[NotMapped]
		public Demographic DemographicForCreateView { get; set; }
	}
}
