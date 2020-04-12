using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HSconnect.Models;

namespace HSconnect.Contracts
{
    public interface IGetCoordinatesRequest
    {
        Task<double> GetLat(string url, Address address);
        Task<double> GetLng(string url, Address address);
        //void GetCoordinatesUsingGeocode(string url, Address address);
        string GetAddressAsURL(Address address);
    }
}
