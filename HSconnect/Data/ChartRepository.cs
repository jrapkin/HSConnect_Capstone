using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class ChartRepository : RepositoryBase<Chart>, IChartRepository
	{
		public ChartRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{
		}
		public ICollection<Chart> GetChartsIncludeAll()
		{
			return FindAll().Include(s => s.SocialWorker).Include(m => m.Member).Include(so => so.ServiceOffered).ToList();

		}
		public ICollection<Chart> GetChartsBySocialWorkerIdIncludeAll(int socialWorkerId)
		{
			return FindByCondition(c => c.SocialWorkerId == socialWorkerId).Include(sw => sw.SocialWorker)
																		   .Include(s => s.ServiceOffered)
																		   .ToList();
		}
		public ICollection<Chart> GetChartsByProvider(int providerId)
		{
			return FindByCondition(c => c.ServiceOffered.ProviderId == providerId).ToList();
		}

		public void CreateChart(Chart chart) => Create(chart);
		public async Task<ICollection<Chart>> GetChartsByMemberId(int? id)
		{
			return await FindByCondition(c => c.MemberId == id).Include(sw => sw.SocialWorker)
																.Include(s => s.ServiceOffered)
																.ToListAsync();
		}
	}
}
