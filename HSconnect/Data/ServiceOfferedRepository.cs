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
		public async Task<ICollection<ServiceOffered>> GetServiceOfferedIncludeAllAsync() => await FindAll()
																								   .Include(p => p.Provider)
																								   .Include(c => c.Category)
																								   .Include(a => a.Address)
																								   .Include(s => s.Service)
																								   .ToListAsync();
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