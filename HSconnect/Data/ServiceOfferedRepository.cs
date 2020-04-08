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
		public ICollection<ServiceOffered> GetServicesOfferedByProvider(int providerId) 
		{
			return FindByCondition(s => s.ProviderId == providerId).ToList();
		}
		public ICollection<ServiceOffered> GetServicesOfferedIncludeAll()
		{
			return FindAll().ToList();
		}
		public ServiceOffered GetServiceOffered(int id)
		{
			return FindByCondition(s => s.Id == id).FirstOrDefault();
		}
	}
}