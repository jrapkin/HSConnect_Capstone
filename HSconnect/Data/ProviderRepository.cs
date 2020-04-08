using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class ProviderRepository : RepositoryBase<Provider>, IProviderRepository
	{
		public ProviderRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)

		{ 
		}
		public Provider GetProvider(int providerId) => FindByCondition(i => i.Id == providerId).SingleOrDefault();
	}
}
