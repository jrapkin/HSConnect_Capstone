using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public interface IPartnershipRepository : IRepositoryBase<Partnership>
	{
		Partnership GetPartnership(int partnershipId);
		ICollection<Partnership> GetPartnershipsTiedToProvider(int providerId);
		void CreatePartnership(Provider provider, ManagedCareOrganization managedCareOrganization);
	}
}
