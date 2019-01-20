using GenericCommerceApiClient.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GenericCommerceApiClient
{
    public static class OrderClient
    {
        private static HttpClient client = new HttpClient();
        private static IConfigurationRoot _configuration;

        static OrderClient()
        {
            IConfigurationBuilder config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("config.json");

            _configuration = config.Build();
            string baseUri = _configuration.GetSection("GenericCommerceApiUris").GetSection("orderUri").Value;
            client.BaseAddress = new Uri(baseUri);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //Get All Orders
        public static async Task<List<Order>> GetOrders()
        {
            var response = client.GetAsync("").Result;
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Order>>();
            }

            return null;
        }

        //Get Order by ID
        public static async Task<Order> GetOrder(int id)
        {
            var response = await client.GetAsync(id.ToString());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Order>();
            }

            return null;
        }

        //Post Order
        public static async Task<HttpResponseMessage> PostOrder(OrderDTO o)
        {
            return await client.PostAsJsonAsync("", o);
        }

        //Update Order
        public static async Task<HttpResponseMessage> UpdateOrder(int id, Order o)
        {
            return await client.PostAsJsonAsync(id.ToString(), o);
        }

        //Delete Order
        public static async Task<HttpResponseMessage> DeleteOrder(int id)
        {
            return await client.DeleteAsync(id.ToString());
        }
    }
}
