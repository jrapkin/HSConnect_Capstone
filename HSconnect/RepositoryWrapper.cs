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
		private IAddressRepository _address;
		private ICategoryRepository _category;
		private IChartRepository _chart;
		private IDemographicRepository _demographic;
		private IManagedCareOrganizationRepository _managedCareOrganization;
		private IMemberRepository _member;
		private IPartnershipRepository _partnership;
		private IServiceRepository _service;
		private IServiceOfferedRepository _serviceOffered;
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
		public IAddressRepository Address
		{
			get
			{
				if (_address == null)
				{
					_address = new AddressRepository(_context);
				}
				return _address;
			}
		}
		public ICategoryRepository Category
		{
			get
			{
				if (_category == null)
				{
					_category = new CategoryRepository(_context);
				}
				return _category;
			}
		}
		public IChartRepository Chart
		{
			get
			{
				if (_chart == null)
				{
					_chart = new ChartRepository(_context);
				}
				return _chart;
			}
		}
		public IDemographicRepository Demographic
		{
			get
			{
				if (_demographic == null)
				{
					_demographic = new DemographicRepository(_context);
				}
				return _demographic;
			}
		}
		public IManagedCareOrganizationRepository ManagedCareOrganization
		{
			get
			{
				if (_managedCareOrganization == null)
				{
					_managedCareOrganization = new ManagedCareOrganizationRepository(_context);
				}
				return _managedCareOrganization;
			}
		}
		public IMemberRepository Member
		{
			get
			{
				if (_member == null)
				{
					_member = new MemberRepository(_context);
				}
				return _member;
			}
		}
		public IPartnershipRepository Partnership
		{
			get
			{
				if (_partnership == null)
				{
					_partnership = new PartnershipRepository(_context);
				}
				return _partnership;
			}
		}
		public IServiceRepository Service
		{
			get
			{
				if (_service == null)
				{
					_service = new ServiceRepository(_context);
				}
				return _service;
			}
		}
		public IServiceOfferedRepository ServiceOffered
		{
			get
			{
				if (_serviceOffered == null)
				{
					_serviceOffered = new ServiceOfferedRepository(_context);
				}
				return _serviceOffered;
			}
		}
		public void Save()
		{
			_context.SaveChanges();
		}
	}
}
