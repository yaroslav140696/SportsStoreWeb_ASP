using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
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
        private IRepository<Product> productRepository;
        public int PageSize = 4;
        public ProductController(IRepository<Product> repos)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("uk-UA");
            productRepository = repos;
        }
        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel model = new ProductListViewModel
            {
                Products = productRepository.Items
                .Where(x => (category == null || x.Category == category))
                .OrderBy(x => x.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
               ,
                PagingInfo = new PagingInfo()
                {
                    Currentpage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? productRepository.Items.Count()
                    : productRepository.Items.Where(x => x.Category == category).Count()
                },
                CurrentCategory = category
            };
            return View(model);
        }
        public ViewResult ProductView(int productID = 1)
        {
            Product product = productRepository.Items.FirstOrDefault(x => x.ProductID == productID);
            return View(product);
        }

        public void AddToLastSeenProduct(int itemID, LastSeen lastSeen)
        {
            var isExist = lastSeen.lastSeen.Any(x => x.ProductID == itemID);
            var product = productRepository.Items.FirstOrDefault(x => x.ProductID == itemID);
            if (isExist)
            {
                lastSeen.lastSeen.Remove(lastSeen.lastSeen.Find(x=>x.ProductID==itemID));
                lastSeen.lastSeen.Add(product);
            }
            else
            {
                if (lastSeen.lastSeen.Count <= 4)
                {
                    lastSeen.lastSeen.Add(product);
                }
                else
                {
                    lastSeen.lastSeen.RemoveAt(0);
                    lastSeen.lastSeen.Add(product);
                }
            }
        }

        public PartialViewResult LastSeenProduct(LastSeen lastSeen)
        {
            return PartialView(lastSeen);
        }
    }
}