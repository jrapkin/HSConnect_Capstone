using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public interface IServiceOfferedRepository : IRepositoryBase<ServiceOffered>
	{
		ICollection<ServiceOffered> GetServicesOfferedByProvider(int providerId);
		ICollection<ServiceOffered> GetServicesOfferedIncludeAll();
		ServiceOffered GetServiceOffered(int id);
		void CreateServiceOffered(Provider provider, Category category, Address address, Demographic demographic, Service service);
	}
}
