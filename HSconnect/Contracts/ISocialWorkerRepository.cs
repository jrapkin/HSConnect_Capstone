using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public interface ISocialWorkerRepository : IRepositoryBase<SocialWorker>
	{
		SocialWorker GetSocialWorkerByUserId(string UserId);
		void CreateSocialWorker(SocialWorker socialWorker);
		SocialWorker GetSocialWorkerById(int socialWorkerId);
	}
}
