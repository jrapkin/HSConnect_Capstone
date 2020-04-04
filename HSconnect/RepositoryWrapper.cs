using HSconnect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
	public class RepositoryWrapper : IRepositoryWrapper
	{
		private ApplicationDbContext _context;
		private IProviderRepository _provider;
		private ISocialWorkerRepository _socialWorker;
		public IProviderRepository Provider
		{
			get
			{
				if(_provider == null)
				{
					_provider = new ProviderRepository(_context);
				}
				return _provider;
			}
		}
		public ISocialWorkerRepository SocialWorker
		{
			get
			{
				if(_socialWorker == null)
				{
					_socialWorker = new SocialWorkerRepository(_context);
				}
				return _socialWorker;
			}
		}
		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
