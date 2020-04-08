using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class SocialWorkerRepository : RepositoryBase<SocialWorker>, ISocialWorkerRepository
	{
		public SocialWorkerRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{ 
		}
		public SocialWorker GetSocialWorker(string socialWorkerUserId) => FindByCondition(i => i.IdentityUserId == socialWorkerUserId).SingleOrDefault();
		public void CreateSocialWorker(SocialWorker socialWorker) => Create(socialWorker);
	}
}
