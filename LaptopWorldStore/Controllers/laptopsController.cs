using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using LaptopWorldStore.Models;

namespace LaptopWorldStore.Controllers
{
    public class laptopsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: laptops
        public ActionResult Index(int page = 1)
        {
            var laptops = db.products.Include(l => l.category).Where(l => l.category.category_id == "laptop");
            return View(laptops.ToList().ToPagedList(page, 10));
        }

         
  
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            laptop laptop = db.laptops.First(l =>l.product_id == id);
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
