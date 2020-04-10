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
		public void CreateServiceOffered(string cost, Provider provider, Category category, Address address, Demographic demographic, Service service)
		{
			ServiceOffered serviceOffered = new ServiceOffered();
			serviceOffered.Cost = cost;
			serviceOffered.ProviderId = provider.Id;
			serviceOffered.CategoryId = category.Id;
			serviceOffered.AddressId = address.Id;
			serviceOffered.DemographicId = demographic.Id;
			serviceOffered.ServiceId = service.Id;
		}
	}
}