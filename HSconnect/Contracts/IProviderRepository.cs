using HSconnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSconnect.Contracts
{
    public interface IProviderRepository : IRepositoryBase<Provider>
    {
        Provider GetProvider(int providerId);
    }
}
