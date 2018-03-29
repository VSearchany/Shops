using Shops.Data;
using Shops.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shops.Controllers
{
    public class ShopController : Controller
    {
        UnitOfWork unitOfWork;
        const int pageSize = 15;

        public ShopController()
        {
            unitOfWork = new UnitOfWork();
        }
        
        [Route("{id:int}")]
        public ActionResult Index(int? id)
        {
            int page = id ?? 0;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Items", GetShopsPage(page));
            }
            return View(GetShopsPage(page));
        }

        private List<Shop> GetShopsPage(int page)
        {
            var shopsToSkip = page * pageSize;
            var shops = unitOfWork.Shops.GetList().Skip(shopsToSkip).Take(pageSize).ToList();
            return shops;
        }

        public ActionResult Details(int id)
        {
            var shop = unitOfWork.Shops.Get(id);
            return View(shop);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Shop shop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Shops.Create(shop);
                    unitOfWork.Save();
                    return RedirectToAction("Index", new { id = 0 });
                }
                return View(shop);
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            Shop shop = unitOfWork.Shops.Get(id);
            if (shop == null)
                return HttpNotFound();
            return View(shop);
        }

        [HttpPost]
        public ActionResult Edit(Shop shop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Shops.Update(shop);
                    unitOfWork.Save();
                    return RedirectToAction("Index", new { id = 0 });
                }
                return View(shop);
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult Delete(int id)
        {
            unitOfWork.Shops.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index", new { id = 0 });
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}