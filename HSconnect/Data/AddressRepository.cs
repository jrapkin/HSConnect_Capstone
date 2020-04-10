using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Data
{
	public class AddressRepository : RepositoryBase<Address>, IAddressRepository
	{
		public AddressRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{ 
		}
		public void CreateAddress(Address address) => Create(address);
		public Address GetAddressById(Address address)
		{
			return FindByCondition(a => a.Id == address.Id).SingleOrDefault();
		}
		public Address GetByAddress(Address address)
		{
			return FindByCondition(a => a.StreetAddress == address.StreetAddress && a.City == address.City && a.County == address.County && a.State == address.State && a.ZipCode == address.ZipCode).SingleOrDefault();
		}
	}
}
