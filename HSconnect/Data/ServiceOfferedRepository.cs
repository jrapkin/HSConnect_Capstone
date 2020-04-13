using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.EntityFrameworkCore;
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
		public async Task<ICollection<ServiceOffered>> GetServiceOfferedIncludeAllAsync()
		{ 
			return await FindAll()
				.Include(p => p.Provider)
				.Include(c => c.Category)
				.Include(a => a.Address)
				.Include(s => s.Service)
				.Include(s => s.Demographic).ToListAsync();
		}
		public ICollection<ServiceOffered> GetServicesOfferedIncludeAll()
		{
			return FindAll().Include(s => s.Address).Include(s => s.Category).Include(s => s.Provider).Include(s => s.Service).Include(s => s.Demographic).ToList();
		}
		public ICollection<ServiceOffered> GetServicesOfferedIncludeAll(int id)
		{
			return FindAll().Include(s => s.Address).Include(s => s.Category).Include(s => s.Provider).Include(s => s.Service).Include(s => s.Demographic).Where(s => s.Id == id).ToList();
		}
		public async Task<ICollection<ServiceOffered>> GetServicesOfferedIncludeAllAsync()
		{
			return await FindAll()
				.Include(p => p.Provider)
				.Include(c => c.Category)
				.Include(a => a.Address)
				.Include(s => s.Service)
				.Include(s => s.Demographic).ToListAsync();
		}
		public async Task<ICollection<ServiceOffered>> GetServicesOfferedIncludeAllAsync(int providerId)
		{
			return await FindAll()
				.Include(p => p.Provider)
				.Include(c => c.Category)
				.Include(a => a.Address)
				.Include(s => s.Service)
				.Include(s => s.Demographic).Where(s => s.ProviderId == providerId).ToListAsync();
		}
		public ServiceOffered GetServiceOffered(int id)
		{
			return FindByCondition(s => s.Id == id).FirstOrDefault();
		}
		public ServiceOffered GetServiceOfferedByIdIncludeAll(int serviceOfferedId)
		{
			return FindByCondition(s => s.Id == serviceOfferedId).Include(p => p.Provider)
				.Include(c => c.Category)
				.Include(a => a.Address)
				.Include(s => s.Service).FirstOrDefault();
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
			Create(serviceOffered);
		}
	}
}