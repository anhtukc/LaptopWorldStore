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
    public class laptopsController : Controller
    {
        private LaptopWorld db = new LaptopWorld();

        public JsonResult GetLaptop(string id)
        {
            var pd = db.products.Select(p => new {
                p.product_id,
                p.product_name,
                p.provider.provider_id,
                p.quantity,
                p.picture,
                p.price,
                p.decription
            }).Where(p => p.product_id == id).FirstOrDefault();
            var laptop = db.laptops.Where(p => p.product_id == id).FirstOrDefault();
          
            return Json(new { Product = pd, Laptop = laptop }, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            laptop laptop = db.laptops.Where(l => l.product_id == id).FirstOrDefault();
            if (laptop == null)
            {
                return HttpNotFound();
            }
            return View(laptop);
        }

        // GET: Admin/laptops/Create
        public ActionResult Create()
        {
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", db.providers.First());
            ViewBag.product_id = new SelectList(db.products, "product_id", "category_id");
            return View();
        }

        // POST: Admin/laptops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public string CreateLaptop(product pd, laptop lt)
        {
            pd.laptops.Add(lt);
            db.products.Add(pd);
            db.SaveChanges();
            return "Thêm thành công ";
        }

        // GET: Admin/laptops/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            laptop laptop = db.laptops.Where(lt=>lt.product_id == id).FirstOrDefault();
            if (laptop == null)
            {
                return HttpNotFound();
            }
            ViewBag.provider_id = new SelectList(db.providers, "provider_id", "provider_name", db.providers.First());

            return View(laptop);
        }

        // POST: Admin/laptops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string EditLaptop(product pd,  laptop lt)
        {
            lt.product_id = pd.product_id;
            db.products.AddOrUpdate(pd);
            db.laptops.AddOrUpdate(lt);
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
