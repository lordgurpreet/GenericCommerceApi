using GenericCommerceApiClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericCommerceApiClientTest
{
    [TestClass]
    public class OrderClientTest
    {
        [TestMethod]
        public void GetAllOrdersTest()
        {
            var o = OrderClient.GetOrders().Result;

            Assert.IsNotNull(o);
        }

        [TestMethod]
        public void GetOrderByIDTest()
        {
            var o = OrderClient.GetOrder(1).Result;

            Assert.IsNotNull(o);
        }
    }
}
