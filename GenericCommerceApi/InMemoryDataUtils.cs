using GenericCommerceApi.Models.Context;
using GenericCommerceApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApi
{
    public class InMemoryDataUtils
    {
        public static void AddTestData(CommerceContext context)
        {
            //Add test users
            context.Customers.Add(new Customer() { CustomerFirstName = "Ric", CustomerLastName = "Flair"});
            context.Customers.Add(new Customer() { CustomerFirstName = "Randy", CustomerLastName = "Savage"});
            context.Customers.Add(new Customer() { CustomerFirstName = "Ricky", CustomerLastName = "Steamboat"});

            //Add product catalogue
            context.Products.Add(new Product() { ProductName = "Bread", ProductPrice = 0.50M });
            context.Products.Add(new Product() { ProductName = "Milk", ProductPrice = 1.20M });
            context.Products.Add(new Product() { ProductName = "Butter", ProductPrice = 2.50M });

            //Add some test orders
            context.Orders.Add(new Order() { OrderFulfilled = true, OrderCustomerId = 1, OrderLineItems = GetOrderItems() });
            context.Orders.Add(new Order() { OrderFulfilled = false, OrderCustomerId = 1, OrderLineItems = GetOrderItems() });
            context.Orders.Add(new Order() { OrderFulfilled = false, OrderCustomerId = 2, OrderLineItems = GetOrderItems() });
            context.Orders.Add(new Order() { OrderFulfilled = true, OrderCustomerId = 3, OrderLineItems = GetOrderItems() });

            context.SaveChanges();
        }

        private static List<LineItem> GetOrderItems()
        {
            return new List<LineItem>() { new LineItem() { ProductId = 1, LineItemQuantity = 1 },
                                                             new LineItem() { ProductId = 2, LineItemQuantity = 3} };
        }
    }
}
