using GenericCommerceApi.Models.Context;
using GenericCommerceApi.Models.Entities;
using GenericCommerceApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericCommerceTest
{
    [TestClass]
    public class OrderServiceTest
    {
        private CommerceContext _context;
        private DbContextOptions<CommerceContext> _options;
        private OrderService _service;

        public OrderServiceTest()
        {
            var builder = new DbContextOptionsBuilder<CommerceContext>();
            builder.UseInMemoryDatabase("TestCommerceDb");
            _options = builder.Options;
        }

        [TestInitialize]
        public void StartUp()
        {
            _context = new CommerceContext(_options);
            _service = new OrderService(_context);

            //Add test users
            _context.Customers.Add(new Customer() { CustomerFirstName = "Ric", CustomerLastName = "Flair" });
            _context.Customers.Add(new Customer() { CustomerFirstName = "Randy", CustomerLastName = "Savage" });
            _context.Customers.Add(new Customer() { CustomerFirstName = "Ricky", CustomerLastName = "Steamboat" });

            //Add product catalogue
            _context.Products.Add(new Product() { ProductName = "Bread", ProductPrice = 0.50M });
            _context.Products.Add(new Product() { ProductName = "Milk", ProductPrice = 1.20M });
            _context.Products.Add(new Product() { ProductName = "Butter", ProductPrice = 2.50M });

            _context.SaveChanges();
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CanAddOrder()
        {
            OrderDTO o = new OrderDTO()
            {
                OrderCustomerId = 1,
                OrderLineItems = new List<LineItemDTO>()
                {
                    new LineItemDTO()
                    {
                        LineItemQuantity = 5,
                        ProductId = 1
                    }
                }
            };

            var x = _service.AddOrder(o);

            Assert.IsNotNull(_context.Orders.Find(1));
        }

        [TestMethod]
        public void AddOrderInvalidUser()
        {
            OrderDTO o = new OrderDTO()
            {
                OrderCustomerId = 8,
                OrderLineItems = new List<LineItemDTO>()
                {
                    new LineItemDTO()
                    {
                        LineItemQuantity = 5,
                        ProductId = 1
                    }
                }
            };


           var x = _service.AddOrder(o);

            Assert.IsNull(_context.Orders.Find(1));
        }

        [TestMethod]
        public void AddOrderUnfulfilledExists()
        {
            OrderDTO o = new OrderDTO()
            {
                OrderCustomerId = 1,
                OrderLineItems = new List<LineItemDTO>()
                {
                    new LineItemDTO()
                    {
                        LineItemQuantity = 5,
                        ProductId = 1
                    }
                }
            };


            var x = _service.AddOrder(o);

            Assert.IsNull(_context.Orders.Find(1));
        }

        [TestMethod]
        public void AddOrderInvalidProduct()
        {
            OrderDTO o = new OrderDTO()
            {
                OrderCustomerId = 1,
                OrderLineItems = new List<LineItemDTO>()
                {
                    new LineItemDTO()
                    {
                        LineItemQuantity = 5,
                        ProductId = 15
                    }
                }
            };


            var x = _service.AddOrder(o);

            Assert.IsNull(_context.Orders.Find(1));
        }
    }
}
