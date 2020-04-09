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
	}
}
