using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        public int PageSize = 4;
        public ProductController(IProductRepository repos)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
            productRepository = repos;
        }
        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = productRepository.Products
                .Where(x => (category == null || x.Category == category))
                .OrderBy(x => x.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
               ,
                PagingInfo = new PagingInfo()
                {
                    Currentpage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? productRepository.Products.Count()
                    : productRepository.Products.Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
    }
}