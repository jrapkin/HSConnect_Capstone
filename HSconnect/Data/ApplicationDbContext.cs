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
						Id = "36a8f961-ab5d-4a34-930d-e9c193fed417",
						Name = "Social Worker",
						NormalizedName = "SOCIAL WORKER"
					},

					new IdentityRole
					{
						Id = "f12efeac-df68-4b53-a60b-ed98c601565f",
						Name = "Provider",
						NormalizedName = "PROVIDER"
					}
				);
			builder.Entity<Category>()
				.HasData(
					new Category
					{
						Id = 1,
						Name = "Healthcare"
					},

					new Category
					{
						Id = 2,
						Name = "RCAC"
					},

					new Category
					{
						Id = 3,
						Name = "Child Welfare"
					},

					new Category
					{
						Id = 4,
						Name = "Crimial Justice/Corrections"
					},

					new Category
					{
						Id = 5,
						Name = "Education"
					},

					new Category
					{
						Id = 6,
						Name = "Mental Health"
					},

					new Category
					{
						Id = 7,
						Name = "Military Support"
					},

					new Category
					{
						Id = 8,
						Name = "Women"
					}
				);
			builder.Entity<Service>()
				.HasData(
					new Service
					{
						Id = 1,
						Name = "Housing",
					},

					new Service
					{
						Id = 2,
						Name = "Meal Plans",
					},

					new Service
					{
						Id = 3,
						Name = "Foster Care",
					},

					new Service
					{
						Id = 4,
						Name = "Child Protection Investigation",
					},

					new Service
					{
						Id = 5,
						Name = "Adoption",
					},

					new Service
					{
						Id = 6,
						Name = "Meal Plans",
					},

					new Service
					{
						Id = 7,
						Name = "Legal Assistance",
					},

					new Service
					{
						Id = 8,
						Name = "Safe Environment",
					},

					new Service
					{
						Id = 9,
						Name = "Rehabilitation Program",
					},

					new Service
					{
						Id = 10,
						Name = "Caregiver Assistance",
					},

					new Service
					{
						Id = 11,
						Name = "Skilled Nursing",
					}
				);
			builder.Entity<ManagedCareOrganization>()
				.HasData(
					new ManagedCareOrganization
					{
						Id = 1,
						Name = "My Choice Family Care",
						AddressId = 1,
					},

					new ManagedCareOrganization
					{
						Id = 2,
						Name = "Independent Care Health Plan",
						AddressId = 2,
					}
				);
			builder.Entity<Address>()
				.HasData(
					new Address
					{
						Id = 1,
						StreetAddress = "10201 West Innovation Drive, Suite 100",
						City = "Wauwatosa",
						State = "WI",
						County = "Milwaukee",
						ZipCode = "53226"
					},

					new Address
					{
						Id = 2,
						StreetAddress = "1555 N Rivercenter Drive, Suite #206",
						City = "Milwaukee",
						State = "WI",
						County = "Milwaukee",
						ZipCode = "53212"
					}
				);
		}
	}
}
