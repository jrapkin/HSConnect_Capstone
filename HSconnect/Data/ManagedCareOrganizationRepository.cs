using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class ManagedCareOrganizationRepository : RepositoryBase<ManagedCareOrganization>, IManagedCareOrganizationRepository
	{
		public ManagedCareOrganizationRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
	}
}
