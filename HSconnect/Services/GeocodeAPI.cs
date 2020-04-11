using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HSconnect.Contracts;
using HSconnect.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace HSconnect.Services
{
    //static?
    public class GeocodeAPI : IGetCoordinatesRequest
    {
        private IRepositoryWrapper _repo;
        public GeocodeAPI(IRepositoryWrapper repo)
        {
            _repo = repo;
        }
        //public async Task<GetCoordinates> GetCoordinates()
        //{
        //    string url = $"https://maps.googleapis.com/maps/api/geocode/json?address=313+plankinton+ave,milwaukee,+wi&key={API_Keys.GeocodeAndGoogleMapsKey}";
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string json = await response.Content.ReadAsStringAsync();
        //        GetCoordinates coordinates = JsonConvert.DeserializeObject<GetCoordinates>(json);
        //        return coordinates;
        //    }
        //    return null;

        //}

        public async void GetCoordinatesUsingGeocode(string url, Address address)
        {
            double lat;
            double lng;
            HttpClient client = new HttpClient();
            using (client)
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string data = await response.Content.ReadAsStringAsync();
                JObject dataAsJObject = JsonConvert.DeserializeObject<JObject>(data);
                lat = Double.Parse(dataAsJObject["results"]["geometry"]["location"]["lat"].ToString());
                lng = Double.Parse(dataAsJObject["results"]["geometry"]["location"]["lng"].ToString());
            }
            address.Lat = lat;
            address.Lng = lng;
            _repo.Address.Update(address);
            _repo.Save();
        }
        public string GetAddressAsURL(Address address)
        {
            string api = "https://maps.googleapis.com/maps/api/geocode/json?address=";
            string streetAddress;
            string city;
            string state;
            string zipcode;
            streetAddress = address.StreetAddress.Replace(' ', '+');
            city = address.City.Replace(' ', '+');
            state = address.State.Replace(' ', '+');
            zipcode = address.ZipCode.Replace(' ', '+');

            string url = api + streetAddress + ",+" + city + ",+" + state + "," + zipcode + $"&key={API_Keys.GeocodeAndGoogleMapsKey}";
            return url;
        }
    }
}
