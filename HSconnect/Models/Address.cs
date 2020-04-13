using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
	public class Address
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[Display(Name="Street Address")]
		public string StreetAddress { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string County { get; set; }
		[Required]
		public string State { get; set; }
		[Required]
		[Display(Name="Zip Code")]
		public string ZipCode { get; set; }
		public double Lat { get; set; }
		public double Lng { get; set; }
	}
}
