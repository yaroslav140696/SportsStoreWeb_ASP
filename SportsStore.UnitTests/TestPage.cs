using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class TestPage
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Items).Returns(new Product[]
          {
                new Product {ProductID = 1, Name = "P1",Category="Cat2"},
                new Product {ProductID = 2, Name = "P2",Category="Cat1"},
                new Product {ProductID = 3, Name = "P3",Category="Cat1"},
                new Product {ProductID = 4, Name = "P4",Category="Cat2"},
                new Product {ProductID = 5, Name = "P5",Category="Cat1"}
          }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;
            Product[] productArray = result.Products.ToArray();
            Assert.IsTrue(productArray.Length == 2);
            Assert.AreEqual(productArray[0].Name, "P4");
            Assert.AreEqual(productArray[1].Name, "P5");
        }
        [TestMethod]
        public void Can_generate_page_links()
        {
            HtmlHelper helper = null;
            PagingInfo pageInfo = new PagingInfo()
            {
                ItemsPerPage = 10,
                TotalItems = 28,
                Currentpage = 2,
            };
            Func<int, string> pageUrlDelegate = x => "Page" + x;
            MvcHtmlString result = helper.PageLinks(pageInfo, pageUrlDelegate);
            Assert.AreEqual(result.ToString(),
                @"<a href=""Page1"">1</a>" +
                @"<a class=""selected"" href=""Page2"">2</a>" +
                @"<a href=""Page3"">3</a>");
        }
        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Items).Returns(new Product[]
          {
                new Product {ProductID = 1, Name = "P1",Category="Cat2"},
                new Product {ProductID = 2, Name = "P2",Category="Cat1"},
                new Product {ProductID = 3, Name = "P3",Category="Cat1"},
                new Product {ProductID = 4, Name = "P4",Category="Cat2"},
                new Product {ProductID = 5, Name = "P5",Category="Cat1"}
          }.AsQueryable());

            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            ProductListViewModel result = (ProductListViewModel)controller.List(null, 2).Model;
            PagingInfo info = result.PagingInfo;
            Assert.AreEqual(info.Currentpage, 2);
            Assert.AreEqual(info.ItemsPerPage, 3);
            Assert.AreEqual(info.TotalItems, 5);
            Assert.AreEqual(info.TotalPages, 2);
        }
        [TestMethod]
        public void Can_Take_Product_With_NUll_Category()
        {
            Mock<IRepository<Product>> mock = new Mock<IRepository<Product>>();
            mock.Setup(m => m.Items).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1",Category="Cat2"},
                new Product {ProductID = 2, Name = "P2",Category="Cat1"},
                new Product {ProductID = 3, Name = "P3",Category="Cat1"},
                new Product {ProductID = 4, Name = "P4",Category="Cat2"},
                new Product {ProductID = 5, Name = "P5",Category="Cat1"}
            }.AsQueryable());
            ProductController controller = new ProductController(mock.Object);
            controller.PageSize = 3;
            Product[] result = ((ProductListViewModel)controller.List("Cat1", 1).Model).Products.ToArray();
            Assert.IsTrue(result[0].Category == "Cat1" && result[0].Name == "P2");
            Assert.AreEqual(result.Length, 3);
        }
    }
}
