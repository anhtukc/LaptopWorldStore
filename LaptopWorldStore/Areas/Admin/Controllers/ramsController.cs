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
using System.Data.Entity.Migrations;

namespace LaptopWorldStore.Areas.Admin.Controllers
{
    public class ramsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

      
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ram ram = db.rams.Where(r => r.product_id == id).FirstOrDefault();
            if (ram == null)
            {
                return HttpNotFound();
            }
            return View(ram);
        }

        // GET: Admin/rams/Create
        public ActionResult Create()
        {
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", db.providers.First());
            return View();
        }

        // POST: Admin/rams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Createram(product pd, ram ram)
        {
            pd.rams.Add(ram);
            db.products.Add(pd);
            db.SaveChanges();
            return "Tạo thành công";
        }

        // GET: Admin/rams/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ram ram = db.rams.Where(r=>r.product_id == id).FirstOrDefault();
            if (ram == null)
            {
                return HttpNotFound();
            }
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", db.providers.First());
            return View(ram);
        }

 
        [HttpPost]
        public string Editram(product pd, ram ram)
        {
            ram.product_id = pd.product_id;
            db.products.AddOrUpdate(pd);
            db.rams.AddOrUpdate(ram);
            db.SaveChanges();
            return "Sửa thành công";
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
