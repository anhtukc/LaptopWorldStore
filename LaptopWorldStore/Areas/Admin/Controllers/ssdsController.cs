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
    public class ssdsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

       
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ssd ssd = db.ssds.Where(s=>s.product_id == id).FirstOrDefault();
            if (ssd == null)
            {
                return HttpNotFound();
            }
            return View(ssd);
        }

        // GET: Admin/ssds/Create
        public ActionResult Create()
        {
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", db.providers.First());
            return View();
        }

        // POST: Admin/ssds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Createssd(product pd, ssd s)
        {
            pd.ssds.Add(s);
            db.products.Add(pd);
            db.SaveChanges();
            return "Tạo thành công";
        }

        // GET: Admin/ssds/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ssd ssd = db.ssds.Where(s => s.product_id == id).FirstOrDefault(); 
            if (ssd == null)
            {
                return HttpNotFound();
            }
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", db.providers.First());
            return View(ssd);
        }

        // POST: Admin/ssds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Editssd(product pd, ssd s)
        {
            s.product_id = pd.product_id;
            db.products.AddOrUpdate(pd);
            db.ssds.AddOrUpdate(s);
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
