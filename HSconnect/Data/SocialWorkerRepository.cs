using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HSconnect.Data
{
	public class SocialWorkerRepository : RepositoryBase<SocialWorker>, ISocialWorkerRepository
	{
		public SocialWorkerRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{ 
		}
		public SocialWorker GetSocialWorkerByUserId(string userId) => FindByCondition(i => i.IdentityUserId == userId).FirstOrDefault();
		public SocialWorker GetSocialWorkerById(int socialWorkerId) => FindByCondition(s => s.Id == socialWorkerId).FirstOrDefault();
		public void CreateSocialWorker(SocialWorker socialWorker) => Create(socialWorker);
	}
}
