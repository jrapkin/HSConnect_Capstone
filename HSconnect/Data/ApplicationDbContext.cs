using System;
using System.Collections.Generic;
using System.Text;
using HSconnect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HSconnect.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
		public DbSet<Address> Addresses { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Chart> Charts { get; set; }
		public DbSet<Demographic> Demographics { get; set; }
		public DbSet<ManagedCareOrganization> ManagedCareOrganizations { get; set; }
		public DbSet<Member> Members { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Partnership> Partnerships { get; set; }
		public DbSet<Provider> Providers { get; set; }
		public DbSet<Service> Services { get; set; }
		public DbSet<ServiceOffered> ServicesOffered { get; set; }
		public DbSet<SocialWorker> SocialWorkers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<IdentityRole>()
				.HasData(
					new IdentityRole
					{
						Name = "Social Worker",
						NormalizedName = "SOCIALWORKER"
					},

					new IdentityRole
					{
						Name = "Provider",
						NormalizedName = "PROVIDER"
					}
				);
		}
	}
}
