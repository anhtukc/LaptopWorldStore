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
    public class ramsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: rams
        public ActionResult Index(int page = 1)
        {
            var rams = db.products.Include(r => r.category).Where(r=>r.category.category_id =="ram");
            return View(rams.ToList().ToPagedList(page, 10));
        }

         
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ram ram = db.rams.First(p=> p.product_id == id);
            if (ram == null)
            {
                return HttpNotFound();
            }
            return View(ram);
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
