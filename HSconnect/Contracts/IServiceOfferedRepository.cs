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
		Task<ICollection<ServiceOffered>> GetServiceOfferedIncludeAllAsync();
		ServiceOffered GetServiceOffered(int id);
	}
}
