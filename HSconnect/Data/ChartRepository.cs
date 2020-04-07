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
			return FindAll().Include(s => s.SocialWorker).Include(m => m.MemberId).Include(so => so.ServiceOffered).ToList();

		}
	}
}
