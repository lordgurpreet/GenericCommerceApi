using GenericCommerceApi.Models;
using GenericCommerceApi.Models.Context;
using GenericCommerceApi.Models.Entities;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApi.Services
{
    public class ProductsService : IProductsService
    {
        private ICommerceContext _context;

        public ProductsService(ICommerceContext context)
        {
            _context = context;
        }

        //Product Retrieval

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        //CRUD Operations

        public async void AddProduct(Product p)
        {
            if(_context.Products.FirstOrDefault(x => x.ProductName.ToUpper() == p.ProductName.ToUpper()) == null)
            {
                _context.Products.Add(p);
                await _context.SaveChangesAsync();
            }
        }

        public async void UpdateProduct(Product p)
        {
            _context.Products.Update(p);
            await _context.SaveChangesAsync();
        }

        public async void DeleteProduct(Product p)
        {
            _context.Products.Remove(p);
            await _context.SaveChangesAsync();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
