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
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualStudio.Web.CodeGeneration.Templating;

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
        public async Task<double> GetLat(string url, Address address)
        {
            double lat;

            HttpClient client = new HttpClient();
            using (client)
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string data = await response.Content.ReadAsStringAsync();
                JObject dataAsJObject = JsonConvert.DeserializeObject<JObject>(data);
                lat = Double.Parse(dataAsJObject["results"][0]["geometry"]["location"]["lat"].ToString());

            }
            return lat;
        }
        public async Task<double> GetLng(string url, Address address)
        {
            double lng;

            HttpClient client = new HttpClient();
            using (client)
            {
                HttpResponseMessage response = await client.GetAsync(url);
                string data = await response.Content.ReadAsStringAsync();
                JObject dataAsJObject = JsonConvert.DeserializeObject<JObject>(data);
                lng = Double.Parse(dataAsJObject["results"][0]["geometry"]["location"]["lng"].ToString());

            }
            return lng;
        }
        //public async void GetCoordinatesUsingGeocode(string url, Address address)
        //{
        //    Address updatedAddress = new Address();
        //    double lat;
        //    double lng;

        //    HttpClient client = new HttpClient();
        //    using (client)
        //    {
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        string data = await response.Content.ReadAsStringAsync();
        //        JObject dataAsJObject = JsonConvert.DeserializeObject<JObject>(data);
        //        lat = Double.Parse(dataAsJObject["results"][0]["geometry"]["location"]["lat"].ToString());
        //        lng = Double.Parse(dataAsJObject["results"][0]["geometry"]["location"]["lng"].ToString());

        //    }
        //    updatedAddress.Id = address.Id;
        //    updatedAddress.StreetAddress = address.StreetAddress;
        //    updatedAddress.County = address.County;
        //    updatedAddress.City = address.City;
        //    updatedAddress.State = address.State;
        //    updatedAddress.ZipCode = address.ZipCode;
        //    updatedAddress.Lat = lat;
        //    updatedAddress.Lng = lng;
        //    _repo.Address.Update(updatedAddress);
        //    _repo.Save();
        //}
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
