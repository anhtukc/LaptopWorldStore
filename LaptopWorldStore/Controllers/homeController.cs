using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LaptopWorldStore.Models;

namespace LaptopWorldStore.Controllers
{
    public class homeController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: home
        public ActionResult Index()
        {
            var products = db.products.Include(p => p.category);
            return View(products.ToList());
        }

        // GET: home/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            product product = db.products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
