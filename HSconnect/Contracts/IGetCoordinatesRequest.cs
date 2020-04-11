using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Models;

namespace HSconnect.Contracts
{
    public interface IGetCoordinatesRequest
    {
        Task<double[]> GetCoordinatesUsingGeocode(string url);
        string GetAddressAsURL(Address address);
    }
}
