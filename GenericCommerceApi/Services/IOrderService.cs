using GenericCommerceApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericCommerceApi.Services
{
    public interface IOrderService
    {
        Task<Order> GetOrder(int id);

        List<Order> GetAllOrders();

        Task<IActionResult> AddOrder(OrderDTO oDTO);

        void UpdateOrder(Order oDTO);

        void DeleteOrder(Order o);

        bool OrderExists(int id);
    }
}
