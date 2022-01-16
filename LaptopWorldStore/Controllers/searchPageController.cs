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
    public class searchPageController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        // GET: searchPage
        public ActionResult Search(string searchString) {
            return RedirectToAction("Index","searchPage", new { searchString = searchString });
        }


        [HttpGet]
        public ActionResult Index(string searchString)
        {
            var links = from l in db.products
                        select l;

            if (!String.IsNullOrEmpty(searchString))
            {
                links = links.Where(s => s.product_name.Contains(searchString));
            }

            return View(links.ToList());
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
