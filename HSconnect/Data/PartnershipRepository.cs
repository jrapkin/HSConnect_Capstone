using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.EntityFrameworkCore;
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
		public ICollection<Partnership> GetPartnershipsTiedToProvider(int providerId) => FindByCondition(p => p.ProviderId == providerId).ToList();
		public ICollection<Partnership> GetAllPartnershipInfo()
		{
			return FindAll().Include(m => m.ManagedCareOrganization).ToList();
		}
		public void CreatePartnership(Provider provider, ManagedCareOrganization managedCareOrganization)
		{
			Partnership partnership = new Partnership();
			partnership.ProviderId = provider.Id;
			partnership.ManagedCareOrganizationId = managedCareOrganization.Id;
		}
	}
}