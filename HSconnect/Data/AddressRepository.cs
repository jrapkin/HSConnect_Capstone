using HSconnect.Contracts;
using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HSconnect.Data
{
	public class AddressRepository : RepositoryBase<Address>, IAddressRepository
	{
		public AddressRepository(ApplicationDbContext applicationDbContext)
			: base(applicationDbContext)
		{ 
		}
		public void CreateAddress(Address address) => Create(address);
		public Address GetAddressById(int addressId)
		{
			return FindByCondition(a => a.Id == addressId).SingleOrDefault();
		}
		public async Task<Address> GetAddressByIdAsync(int? addressId)
		{
			return await FindByCondition(a => a.Id == addressId).FirstOrDefaultAsync();
		}
		public Address GetByAddress(Address address)
		{
			return FindByCondition(a => a.StreetAddress == address.StreetAddress && a.City == address.City && a.County == address.County && a.State == address.State && a.ZipCode == address.ZipCode).SingleOrDefault();
		}
		public async Task<Address> GetByAddressAsync(Address address)
		{
			return await FindByCondition(a => a.StreetAddress == address.StreetAddress && a.City == address.City && a.County == address.County && a.State == address.State && a.ZipCode == address.ZipCode).FirstOrDefaultAsync();
		}
	}
}
