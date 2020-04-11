using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HSconnect.Contracts;
using HSconnect.Models;

namespace HSconnect.Services
{
    //static?
    public class GeocodeAPI : IGetCoordinatesRequest
    {
        public GeocodeAPI()
        {

        }
        public async Task<GetCoordinates> GetCoordinates()
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address=313+plankinton+ave,milwaukee,+wi&key={API_Keys.GeocodeAndGoogleMapsKey}";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                GetCoordinates coordinates = JsonConvert.DeserializeObject<GetCoordinates>(json);
                return coordinates;
            }
            return null;

        }

        //public async Task<double[]> GetCoordinatesUsingGeocode(string url)
        //{
        //    HttpClient client = new HttpClient();
        //    using(client)
        //    {
        //        HttpResponseMessage response = await client.GetAsync(url);
        //        string data = await response.Content.ReadAsStringAsync();
        //        JObject dataAsJObject = JsonConvert.DeserializeObject<JObject>(data);
        //        double lat = 
        //    }
        //}
    }
}
