using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericCommerceApi.Models.Context;
using GenericCommerceApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenericCommerceApi.Services
{
    public class OrderService : IOrderService
    {
        private ICommerceContext _context;

        public OrderService(ICommerceContext context)
        {
            _context = context;
        }

        //Order Retrieval

        public List<Order> GetAllOrders()
        {
            return _context.Orders
                .Include(order => order.OrderCustomer)
                .Include(order => order.OrderLineItems)
                    .ThenInclude(lineItem => lineItem.Product)
                .ToList();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Orders
                .Include(order => order.OrderCustomer)
                .Include(order => order.OrderLineItems)
                    .ThenInclude(lineItem => lineItem.Product)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        //CRUD Operations

        public async Task<IActionResult> AddOrder(OrderDTO oDTO)
        {
            Order o = new Order()
            {
                OrderFulfilled = false,
                OrderCustomerId = oDTO.OrderCustomerId
            };
            
            //Validate the User
            var customer = GetCustomer(oDTO.OrderCustomerId);
            if (customer == null)
                return new BadRequestObjectResult("Invalid User ID");
            
            o.OrderCustomer = customer;

            if (CustomerHasOpenOrder(oDTO.OrderCustomerId))
                return new BadRequestObjectResult("Customer already has an active order");

            //Validate Products
            foreach(LineItemDTO l in oDTO.OrderLineItems)
            {
                LineItem line = new LineItem()
                { ProductId = l.ProductId };

                if (l.LineItemQuantity < 1)
                    return new BadRequestObjectResult("Invalid quantity");

                    line.LineItemQuantity = l.LineItemQuantity;

                var product = GetProduct(l.ProductId);

                if (product == null)
                    return new BadRequestObjectResult("Invalid Product");

                line.Product = product;

                o.OrderLineItems.Add(line);
            }

            //Add the order
            _context.Orders.Add(o);
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        public async void UpdateOrder(Order o)
        {
            _context.Orders.Update(o);
            await _context.SaveChangesAsync();
        }

        public async void DeleteOrder(Order o)
        {
            _context.Orders.Remove(o);
            await _context.SaveChangesAsync();
        }

        public bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

        private Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }

        private Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        private bool CustomerHasOpenOrder(int id)
        {
            if (_context.Orders.FirstOrDefault(x => !x.OrderFulfilled && x.OrderCustomerId == id) != null)
                return true;
            else
                return false;
        }
    }
}
