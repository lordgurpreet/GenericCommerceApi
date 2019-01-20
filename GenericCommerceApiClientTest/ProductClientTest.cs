using GenericCommerceApiClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericCommerceApiClientTest
{
    [TestClass]
    public class ProductClientTest
    {
        [TestMethod]
        public void GetAllProductsTest()
        {
            var p = ProductClient.GetProducts().Result;

            Assert.IsNotNull(p);
        }

        [TestMethod]
        public void GetProductByIDTest()
        {
            var p = ProductClient.GetProduct(1).Result;

            Assert.IsNotNull(p);
        }
    }
}
