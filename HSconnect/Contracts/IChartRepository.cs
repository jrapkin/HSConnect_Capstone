using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public interface IChartRepository : IRepositoryBase<Chart>
	{
		ICollection<Chart> GetChartsIncludeAll();
		ICollection<Chart> GetChartsBySocialWorkerIdIncludeAll(int socialWorkerId);
		ICollection<Chart> GetChartsByProvider(int providerId);
		Task<ICollection<Chart>> GetChartsByMemberId(int? id);
		void CreateChart(Chart chart);
	}
}
