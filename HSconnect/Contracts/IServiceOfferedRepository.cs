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
		ICollection<ServiceOffered> GetServicesOfferedIncludeAll(int id);
		Task<ICollection<ServiceOffered>> GetServicesOfferedIncludeAllAsync();
		Task<ICollection<ServiceOffered>> GetServicesOfferedIncludeAllAsync(int providerId);
		ServiceOffered GetServiceOffered(int id);
		void CreateServiceOffered(string cost, Provider provider, Category category, Address address, Demographic demographic, Service service);
		ServiceOffered GetServiceOfferedByIdIncludeAll(int serviceOfferedId);
	}
}
