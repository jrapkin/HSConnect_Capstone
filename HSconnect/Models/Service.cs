﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Models
{
	public class Service
	{
		[Key]
		[Display(Name="Service")]
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
