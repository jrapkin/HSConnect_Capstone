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
		ICollection<Chart> GetChartsByProvider(int providerId);
	}
}
