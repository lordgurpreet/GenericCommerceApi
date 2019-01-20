using GenericCommerceApiClient.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GenericCommerceApiClient
{
    public class ProductClient
    {
        private static HttpClient client = new HttpClient();
        private static IConfigurationRoot _configuration;

        static ProductClient()
        {
            IConfigurationBuilder config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("config.json");

            _configuration = config.Build();
            string baseUri = _configuration.GetSection("GenericCommerceApiUris").GetSection("productUri").Value;
            client.BaseAddress = new Uri(baseUri);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        //Get All Products
        public static async Task<List<Product>> GetProducts()
        {
            var response = client.GetAsync("").Result;
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<List<Product>>();
            }

            return null;
        }

        //Get Product by ID
        public static async Task<Product> GetProduct(int id)
        {
            var response = await client.GetAsync(id.ToString());
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<Product>();
            }

            return null;
        }

        //Post Product
        public static async Task<HttpResponseMessage> PostProduct(Product p)
        {
            return await client.PostAsJsonAsync("", p);
        }

        //Update Product
        public static async Task<HttpResponseMessage> PostProduct(int id, Product p)
        {
            return await client.PostAsJsonAsync(id.ToString(), p);
        }

        //Delete Product
        public static async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            return await client.DeleteAsync(id.ToString());
        }

    }
}
