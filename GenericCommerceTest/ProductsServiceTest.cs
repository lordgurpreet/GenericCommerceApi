using GenericCommerceApi.Models.Context;
using GenericCommerceApi.Models.Entities;
using GenericCommerceApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericCommerceTest
{
    [TestClass]
    public class ProductsServiceTest
    {
        private CommerceContext _context;
        private DbContextOptions<CommerceContext> _options;
        private ProductsService _service;

        public ProductsServiceTest()
        {
            var builder = new DbContextOptionsBuilder<CommerceContext>();
            builder.UseInMemoryDatabase("TestCommerceDb");
            _options = builder.Options;
        }

        [TestInitialize]
        public void Startup()
        {
            _context = new CommerceContext(_options);
            _service = new ProductsService(_context);
        }

        [TestCleanup]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void CanAddProduct()
        {
            var product = new Product() { ProductName = "Bread", ProductPrice = 0.50M };
            _service.AddProduct(product);

            Assert.IsTrue(_context.Products.ContainsAsync(product).Result);
        }

        [TestMethod]
        public void CannotAddDuplicateProduct()
        {
            var product = new Product() { ProductName = "Bread", ProductPrice = 0.50M };
            _service.AddProduct(product);
            _service.AddProduct(product);

            Assert.IsTrue(_context.Products.CountAsync(x => x.ProductName == "Bread").Result == 1);
        }

        [TestMethod]
        public void CanUpdateProduct()
        {
            var product = new Product() { ProductName = "Bread", ProductPrice = 0.50M };
            _service.AddProduct(product);

            product.ProductPrice = 1.00M;
            _service.UpdateProduct(product);

            Assert.IsTrue(_context.Products.FirstOrDefaultAsync(x => x.ProductName == "Bread").Result.ProductPrice == 1.00M);
        }

        [TestMethod]
        public void CanDeleteProduct()
        {
            var product = new Product() { ProductName = "Bread", ProductPrice = 0.50M };
            _service.AddProduct(product);
            _service.DeleteProduct(product);

            Assert.IsNull(_context.Products.FirstOrDefaultAsync(x => x.ProductName == "Bread").Result);
        }
    }
}
