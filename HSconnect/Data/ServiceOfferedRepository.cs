using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class ServiceOfferedRepository : RepositoryBase<ServiceOffered>, IServiceOfferedRepository
	{
		public ServiceOfferedRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
	}
}