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
    public class ssdsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: ssds
        public ActionResult Index(int page = 1)
        {
            var ssds = db.ssds.Include(s => s.product).Where(s=> s.product.category.category_id == "ssd");
            return View(ssds.ToList().ToPagedList(page, 10));
        }

       
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ssd ssd = db.ssds.First(s => s.product_id == id);
            if (ssd == null)
            {
                return HttpNotFound();
            }
            return View(ssd);
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
