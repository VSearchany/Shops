using Shops.Data;
using Shops.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shops.Controllers
{
    [RoutePrefix("Product/{shopId:int}")]
    public class ProductController : Controller
    {
        UnitOfWork unitOfWork;
        const int pageSize = 10;

        public ProductController()
        {
            unitOfWork = new UnitOfWork();
        }
        
        [Route("Index/{id:int}")]
        public ActionResult Index(int shopId, int? id)
        {
            ViewBag.shopId = shopId;
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", GetProductsPage(shopId, page));
            }
            return View(GetProductsPage(shopId, page));
        }

        public List<Product> GetProductsPage(int shopId, int page = 1)
        {
            var itemsToSkip = page * pageSize;
            var products = unitOfWork.Products.GetList().Where(item => item.ShopId == shopId).ToList().Skip(itemsToSkip).Take(pageSize).ToList();
            return products;
        }

        [Route("Details/{id:int}")]
        public ActionResult Details(int id, int shopId)
        {
            var product = unitOfWork.Products.Get(id);
            return View(product);
        }

        [Route("Create")]
        public ActionResult Create(int shopId)
        {
            return View();
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Product product, int shopId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Products.Create(product);
                    unitOfWork.Save();
                    int id = 0;
                    return RedirectToAction("Index", new { shopId, id });
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, int shopId)
        {
            Product product = unitOfWork.Products.Get(id);
            if (product == null)
                return HttpNotFound();
            return View(product);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(Product product, int shopId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Products.Update(product);
                    unitOfWork.Save();
                    int id = 0;
                    return RedirectToAction("Index", new { shopId, id });
                }
                return View(product);
            }
            catch
            {
                return View();
            }
        }

        [Route("Delete/{id:int}")]
        public ActionResult Delete(int id, int shopId)
        {
            unitOfWork.Products.Delete(id);
            unitOfWork.Save();
            id = 0;
            return RedirectToAction("Index", new { shopId, id });
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
