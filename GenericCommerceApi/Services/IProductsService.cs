using GenericCommerceApi.Models.Context;
using GenericCommerceApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApi.Services
{
    public interface IProductsService
    {
        Task<Product> GetProduct(int id);

        List<Product> GetAllProducts();

        void AddProduct(Product p);

        void UpdateProduct(Product p);

        void DeleteProduct(Product p);

        bool ProductExists(int id);
    }
}
