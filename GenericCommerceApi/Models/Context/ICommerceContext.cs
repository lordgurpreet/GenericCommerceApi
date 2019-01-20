using GenericCommerceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApi.Models.Context
{
    public interface ICommerceContext
    {
        DbSet<Product> Products { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Order> Orders { get; set; }
        DbSet<LineItem> LineItems { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
