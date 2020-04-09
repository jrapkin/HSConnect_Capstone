using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class PartnershipRepository : RepositoryBase<Partnership>, IPartnershipRepository
	{
		public PartnershipRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
		public Partnership GetPartnership(int partnershipId) => FindByCondition(i => i.Id == partnershipId).SingleOrDefault();
	}
}