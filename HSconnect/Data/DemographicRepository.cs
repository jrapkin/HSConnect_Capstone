using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class DemographicRepository : RepositoryBase<Demographic>, IDemographicRepository
	{
		public DemographicRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
		public ICollection<Demographic> GetAllDemographics()
		{
			return FindAll().ToList();
		}
		public void CreateDemographic(Demographic demographic)
		{
			Create(demographic);
		}
	}
}
