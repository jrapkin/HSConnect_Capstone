using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public interface IRepositoryWrapper
	{
		ISocialWorkerRepository SocialWorker { get; }
		IProviderRepository Provider { get; }
		IServiceOfferedRepository ServiceOffered { get; }
		IChartRepository Chart { get; }
		IPartnershipRepository Partnership { get; }
		IMemberRepository Member { get; }
		IAddressRepository Address { get; }
		IServiceRepository Service { get; }
		void Save();
	}
}
